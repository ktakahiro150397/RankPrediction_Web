USE [RankPrediction]
GO

/****** Object:  StoredProcedure [ml_predict].[py_predict_rank]    Script Date: 8/21/2021 6:02:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [ml_predict].[py_predict_rank] (@p_id int,@model varchar(100))
AS
BEGIN
    DECLARE @py_model varbinary(max) = (select model from rank_py_models where model_name = @model);

	Declare @p_script nvarchar(max) = N'import numpy as np
from numpy.lib.twodim_base import triu_indices_from
import pandas as pd
from sklearn.svm import SVC
from sklearn.model_selection import cross_validate, StratifiedKFold
import pickle

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
		self.C = 0.85
		self.kernel = "rbf"
		self.gamma = 0.01
	def teature_extractor(self):
		self.x = self.dataframe[self.dataframe["id"] != self.id].iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
		self.y = self.dataframe[self.dataframe["id"] != self.id].iloc[:,[-1]]#最後のcolはランクラベル
		print(self.y)
	def test_extractor(self):
		self.x_test = self.dataframe[self.dataframe["id"] == self.id].iloc[-1,1:-1]#dataframeの最後を抜き出す
		self.y_test = self.dataframe[self.dataframe["id"] == self.id].iloc[-1,-1]
		print(self.x_test)
	def cross_val(self):#cross validationのスコアを返す
		skf = StratifiedKFold(shuffle=True, random_state=0, n_splits=3)
		scores = cross_validate(self.clf,self.x,self.y.values.reshape(-1,), cv= skf, return_train_score=True)
		#print(scores)
		print(np.mean(scores["test_score"]))
		return np.mean(scores["test_score"])
	def weight(self,unestimation):
		d = [(-1, 98), (0, 844), (1, 1028)]
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
		if result==22:
			result = 21
		elif result==-1:
			result = 0
		return result
	def estimator(self):
		self.teature_extractor()
		self.test_extractor()
		self.clf = pickle.loads(py_model)
		score = self.cross_val()#正答率のようなもの
		if score < self.limit :
			return self.weight(self.y_test)#重みのついたランダムを返す
		else:
			pred_y = self.clf.predict(self.x_test.values.reshape([1,-1]))
			return pred_y[0]
		

df=rank_score_data
id= int(#INPUT_USER_ID#)
print(df)
rank = mlforrank(df, id)
rank_id = pd.DataFrame(np.array([rank.estimator()]))
'

Declare @p_replacedScript nvarchar(max) = REPLACE(@p_script,'#INPUT_USER_ID#',@p_id)

    EXEC sp_execute_external_script 
    @language = N'Python'
    , @script = @p_replacedScript
    , @input_data_1 = N'Select "id", "kill_death_ratio", "average_damage", "match_counts", "is_party", "rank_id"  from [RankPrediction].[ml_predict].[prediction_data] '
    , @input_data_1_name = N'rank_score_data'
	, @output_data_1_name = N'rank_id'
    , @params = N'@py_model varbinary(max)'
    , @py_model = @py_model
    with result sets (("rank_id" int));
END;
GO


