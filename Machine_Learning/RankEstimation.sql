USE [RankPrediction_Production]
GO
/****** Object:  StoredProcedure [ml_predict].[py_predict_rank]    Script Date: 2021/11/14 0:55:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [ml_predict].[py_predict_rank] (@p_id int,@season_id int,@is_matchcounts_contain bit)
AS
BEGIN
    DECLARE @py_model varbinary(max) = (
		SELECT 
			model_data
		FROM rank_py_models
		WHERE
			season_id = @season_id
		AND is_matchcounts_contain = @is_matchcounts_contain
	)

	Declare @p_SelectStr nvarchar(max)
	IF @is_matchcounts_contain = 'TRUE'
	BEGIN
		SET @p_SelectStr = N'Select "id", "kill_death_ratio", "average_damage", "match_counts", "is_party", "rank_id"  from [ml_predict].[prediction_data]'
	END
	ELSE
	BEGIN
		SET @p_SelectStr = N'Select "id", "kill_death_ratio", "average_damage", "is_party", "rank_id"  from [ml_predict].[prediction_data]'
	END

	

	Declare @p_script nvarchar(max) = N'import numpy as np
from numpy.lib.twodim_base import triu_indices_from
import pandas as pd
from sklearn.svm import SVC
from sklearn.model_selection import cross_validate, StratifiedKFold
import pickle
import time
import datetime

start=time.time()
class mlforrank(object):
	"""
	正答率はmodelを読み込んだ後に出す
	教師データを分割し未知データのほうの正答率がある程度いくまでは例の手順
	self.clfは答えを返す用
	"""
	def __init__(self, dataframe, id):
		super(mlforrank, self).__init__()
		self.dataframe = dataframe
		self.id = id #入力したユーザーのid
		self.limit = 0.8 #正答率がこれ以下なら入ってきたまま返す
		self.nsplit = 2 #cvとかgridとかでデータをスプリットする個数
		self.C = 0.85
		self.kernel = "rbf"
		self.gamma = 0.01
		self.q1 = 0.001 #下側の外れ値をどれだけ省くか
		self.q2 = 0.975 #上側の外れ値をどれだけ省くか。上位 (1-q2)*100 % のデータを外れ値として省く
		self.userrank = self.dataframe[self.dataframe["id"] == self.id].iloc[-1,-1]#計算するidのランク
		self.norank = self.userrank == 22
		self.rank_col_name = self.dataframe.columns.values[-1]#ランクが入っているcolの名前
		print("rank conuts")
		print(self.dataframe[self.rank_col_name].value_counts())
		self.number_of_members_in_each_rank = self.dataframe[self.rank_col_name].value_counts().min()#データベースに入っているランクの中で一番少ないやつの頻度
		self.do_split = self.number_of_members_in_each_rank > self.nsplit + 1
		self.do_est = self.number_of_members_in_each_rank >= self.nsplit
		self.dfbool = self.dataframe[self.dataframe[self.rank_col_name] == self.userrank]#計算するidのランクと一致するrowのみで構成されるdf
		self.unique = len(self.dfbool)<=1 #計算するidのランクがそいつだけしか無いか.T/F
	def teature_extractor(self):
		for i in self.dataframe.columns.values[1:-2]:
			self.flier(i)
		print("外れ値削除後")
		print(self.dataframe)
		self.x = self.dataframe[self.dataframe["id"] != self.id].iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
		self.y = self.dataframe[self.dataframe["id"] != self.id].iloc[:,[-1]]#最後のcolはランクラベル
		print(self.y)
	def flier(self,key):#外れ値削除
		q1 = self.dataframe[key].quantile(self.q1)
		q2 = self.dataframe[key].quantile(self.q2)
		max = q2
		min = q1
		self.dataframe = self.dataframe[(self.dataframe[key] <= max)]
		self.dataframe = self.dataframe[(self.dataframe[key] >= min)]
	def test_extractor(self):
		self.x_test = self.dataframe[self.dataframe["id"] == self.id].iloc[-1,1:-1]#dataframeの最後を抜き出す
		self.y_test = self.userrank
		print(self.x_test)
	def cross_val(self):#cross validationのスコアを返す
		skf = StratifiedKFold(shuffle=True, random_state=0, n_splits=self.nsplit)
		scores = cross_validate(self.clf,self.x,self.y.values.reshape(-1,), cv= skf, return_train_score=True)
		#print(scores)
		print(np.mean(scores["test_score"]))
		return np.mean(scores["test_score"])
	def fitting_score(self):
		return self.clf.score(self.x, self.y)
	def weight(self,unestimation):
		d = [(0, 98), (1, 844), (2, 1028)]
		a, w = zip(*d)
		#print(a, w)
		w2 = np.array(w) / sum(w)
		v = np.random.choice(a, p=w2)
		"""
		print(v)
		from collections import Counter
		c = [ np.random.choice(a, p=w2) for i in range(sum(w)) ]
		print(Counter(c))
		"""
		result = unestimation+v
		if result>=22:
			result = 21
		elif result<=-1:
			result = 0
		return result
	def estimator(self):
		if self.unique and not self.norank:
			print(self.unique)
			return self.weight(self.userrank)#重みのついたランダムを返す
		self.test_extractor()
		self.teature_extractor()
		self.clf = pickle.loads(py_model)
		if self.norank:	
			pred_y = self.clf.predict(self.x_test.values.reshape([1,-1]))
			return pred_y[0]
		if self.do_split:
			score = self.cross_val()#正答率のようなもの
		elif self.do_est:
			score = self.fitting_score()
		else :
			return self.weight(self.userrank)
		print(score)
		if score < self.limit :
			return self.weight(self.userrank)#重みのついたランダムを返す
		else:
			pred_y = self.clf.predict(self.x_test.values.reshape([1,-1]))
			print("predicted")
			return pred_y[0]
		
df=rank_score_data
id= int(#INPUT_USER_ID#)
print(df)
rank = mlforrank(df, id)
end=time.time()-start
print("total estimation time:{}".format(end)+"[sec]")
dt_now=datetime.datetime.now()
print("estimation ended:{}".format(dt_now))
rank_id = pd.DataFrame(np.array([rank.estimator()]))
'

Declare @p_replacedScript nvarchar(max) = REPLACE(@p_script,'#INPUT_USER_ID#',@p_id)

    EXEC sp_execute_external_script 
    @language = N'Python'
    , @script = @p_replacedScript
    , @input_data_1 = @p_SelectStr
    , @input_data_1_name = N'rank_score_data'
	, @output_data_1_name = N'rank_id'
    , @params = N'@py_model varbinary(max)'
    , @py_model = @py_model
    with result sets (("rank_id" int));
END;