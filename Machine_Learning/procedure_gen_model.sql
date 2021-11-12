USE [RankPrediction_Production]
GO
/****** Object:  StoredProcedure [ml_predict].[generate_rank_py_model]    Script Date: 2021/11/13 1:45:41 ******/
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


start_model=time.time()
class gen_model(object):
	"""
	grid search (cross_validation)でハイパーパラメータ達を決める
	教師データを分割し未知データのほうの正答率がある程度いくまでは例の手順
	self.classifierはgrid search 用
	self.clfは答えを返す用.pickleで保存
	"""
	def __init__(self, dataframe):
		super(gen_model, self).__init__()
		self.dataframe = dataframe
		self.nsplit = 3 #cvとかgridとかでデータをスプリットする個数
		self.C = 0.85
		self.kernel = "rbf"
		self.gamma = 0.01
		self.q1 = 0.025 #下側の外れ値をどれだけ省くか
		self.q2 = 0.975 #上側の外れ値をどれだけ省くか。上位 (1-q2)*100 % のデータを外れ値として省く
		self.rank_col_name = self.dataframe.columns.values[-1]#ランクが入っているcolの名前
		print("rank conuts")
		print(self.dataframe[self.rank_col_name].value_counts())
		self.number_of_members_in_each_rank = self.dataframe[self.rank_col_name].value_counts().min()#データベースに入っているランクの中で一番少ないやつの頻度
		self.do_grid = self.number_of_members_in_each_rank > self.nsplit + 10 #gridするか否か.T/F.+している数値はサンプル数に応じ変更する
	def teature_extractor(self):
		for i in self.dataframe.columns.values[1:-2]:
			self.flier(i)
		print("外れ値削除後")
		print(self.dataframe)
		self.x = self.dataframe.iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
		self.y = self.dataframe.iloc[:,[-1]]#最後のcolはランクラベル
		print(self.y)
	def flier(self,key):#外れ値削除
		q1 = self.dataframe[key].quantile(self.q1)
		q2 = self.dataframe[key].quantile(self.q2)
		max = q2
		min = q1
		self.dataframe = self.dataframe[(self.dataframe[key] <= max)]
		self.dataframe = self.dataframe[(self.dataframe[key] >= min)]
	def grid(self):#ハイパーパラメタの決定
		grid_start=time.time()
		tuned_parameters=[{"C":[0.85,1,5,10],"kernel":[self.kernel],"gamma":[1,0.1,0.001,0.0001]}]
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