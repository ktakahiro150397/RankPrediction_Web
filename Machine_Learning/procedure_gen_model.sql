USE [RankPrediction_Production]
GO
/****** Object:  StoredProcedure [ml_predict].[generate_rank_py_model]    Script Date: 2021/11/15 1:37:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [ml_predict].[generate_rank_py_model] (@p_selectStr nvarchar(max),@trained_model varbinary(max) OUTPUT)
AS
BEGIN
    EXECUTE sp_execute_external_script
      @language = N'Python'
    , @script = N'
import numpy as np
from numpy.lib.twodim_base import triu_indices_from
import pandas as pd
from sklearn.svm import SVC
from sklearn.model_selection import GridSearchCV
import pickle
import time
import datetime
from sklearn.preprocessing import StandardScaler

start_model=time.time()
class gen_model(object):
	"""
	grid search (cross_validation)でハイパーパラメータ達を決める
	教師データを分割し未知データのほうの正答率がある程度いくまでは例の手順
	self.classifierはgrid search 用
	self.clfは答えを返す用.pickleで保存
	"""
	def __init__(self, dataframe):
		print("This is model generator")
		super(gen_model, self).__init__()
		self.dataframe = dataframe
		self.q1 = 0.04 #下側の外れ値をどれだけ省くか
		self.q2 = 0.92 #上側の外れ値をどれだけ省くか。上位 (1-q2)*100 % のデータを外れ値として省く
		rank_id_list=self.dataframe["rank_id"].unique()
		for i in self.dataframe.columns.values[1:-2]:
			for rank in rank_id_list:
				self.flier(i, rank)
		self.standardizatoin_normalization()
		self.nsplit = 3 #cvとかgridとかでデータをスプリットする個数
		self.C = 10000
		self.kernel = "rbf"
		self.gamma = "scale"

		self.rank_col_name = self.dataframe.columns.values[-1]#ランクが入っているcolの名前
		print("rank conuts")
		count_df = self.dataframe[self.rank_col_name].value_counts()
		print(count_df)
		res = count_df.index[count_df > 1]#データ数が1個以上のランクのリスト
		print(res)
		que = "("
		for re in res:
			que = que+ "rank_id == " + str(re) + " or "
		que = que[:-4] + ")"
		print(que)
		self.dataframe = self.dataframe.query(que)
		print("rank conuts")
		count_df = self.dataframe[self.rank_col_name].value_counts()
		print(count_df)
		self.number_of_members_in_each_rank = self.dataframe[self.rank_col_name].value_counts().min()#データベースに入っているランクの中で一番少ないやつの頻度
		self.do_grid = self.number_of_members_in_each_rank > self.nsplit + 10 #gridするか否か.T/F.+している数値はサンプル数に応じ変更する
	def standardizatoin_normalization(self):
		sc = StandardScaler()
		new_cols = [col for col in self.dataframe.columns if not (col == "id" or col == "rank_id" or col == "is_party")]
		def_cols = [col for col in self.dataframe.columns if col == "id" or col == "rank_id" or col == "is_party"]
		std = sc.fit_transform(self.dataframe[new_cols])
		dfa = pd.DataFrame(std, columns=new_cols)
		dfb = pd.concat([self.dataframe[def_cols].reset_index(drop=True), dfa], axis = 1)
		self.dataframe = dfb.reindex(columns = self.dataframe.columns)
	def teature_extractor(self):
		print("外れ値削除後")
		print(self.dataframe)
		self.x = self.dataframe.iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
		self.y = self.dataframe.iloc[:,[-1]]#最後のcolはランクラベル
		print(self.y)
	def flier(self,key, rank):#指定id以外において外れ値削除
		dfa = self.dataframe[self.dataframe["rank_id"]==rank]#外れ値はランクごとに計算する
		q1 = dfa[key].quantile(self.q1)
		q2 = dfa[key].quantile(self.q2)
		max = q2
		min = q1
		self.dataframe = self.dataframe[(self.dataframe["rank_id"]!=rank) | ((self.dataframe[key] <= max) & (self.dataframe["rank_id"]==rank))]
		self.dataframe = self.dataframe[(self.dataframe["rank_id"]!=rank) | ((self.dataframe[key] >= min) & (self.dataframe["rank_id"]==rank))]
	def grid(self):#ハイパーパラメタの決定
		grid_start=time.time()
		tuned_parameters=[{"C":[0.85,1,10,100,1000,10000],"kernel":[self.kernel],"gamma":["scale",1,0.1,0.001,0.0001]}]
		self.classifier = GridSearchCV(
			SVC(),
			tuned_parameters,
			cv=self.nsplit,
		)
		self.classifier.fit(self.x,self.y.values.reshape(-1,))
		#print(self.classifier.cv_results_)
		#print(self.classifier.best_params_)
		self.C = self.classifier.best_params_["C"]
		self.kernel=self.classifier.best_params_["kernel"]
		self.gamma=self.classifier.best_params_["gamma"]
		grid_end=time.time()-grid_start
		print("grid time:{}".format(grid_end)+"[sec]")
	def generator(self):
		self.teature_extractor()
		if self.do_grid:
			self.grid()
		self.clf = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
		start_fit=time.time()
		self.clf.fit(self.x,self.y.values.reshape(-1,))
		end_fit=time.time()-start_fit
		print("fitting time:{}".format(end_fit)+"[sec]")
		return self.clf
		

df=rank_train_data
print(df)
rank = gen_model(df)
clf = rank.generator()
end_model=time.time()-start_model
print("model total time:{}".format(end_model)+"[sec]")
dt_now=datetime.datetime.now()
print("model generation ended:{}".format(dt_now))
trained_model = pickle.dumps(clf)
'
	, @input_data_1 = @p_selectStr
    , @input_data_1_name = N'rank_train_data'
    , @params = N'@trained_model varbinary(max) OUTPUT'
    , @trained_model = @trained_model OUTPUT;
END;