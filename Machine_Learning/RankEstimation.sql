DROP TABLE IF EXISTS rank_py_models;
GO
CREATE TABLE rank_py_models (
                model_name VARCHAR(30) NOT NULL DEFAULT('default model') PRIMARY KEY,
                model VARBINARY(MAX) NOT NULL
);
GO

--Create a table to store the predictions in
DROP TABLE IF EXISTS [dbo].[py_rental_predictions];
GO
CREATE TABLE [dbo].[py_rental_predictions](
    [Rank_Predicted] [int] NULL,
    [Rank_Actual] [int] NULL,
    [season_id] [int] NULL,
    [rank_id] [int] NULL,
    [kill_death_ratio] [decimal(4,2)] NULL,
    [average_damage] [decimal(7,2)] NULL,
    [match_counts] [int] NULL,
    [is_party] [bit] NULL
) ON [PRIMARY]
GO

DROP PROCEDURE IF EXISTS generate_rank_py_model;
go
CREATE PROCEDURE generate_rank_py_model (@trained_model varbinary(max) OUTPUT)
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
		self.C = 0.85
		self.kernel = 'rbf'
		self.gamma = 0.01
	def teature_extractor(self):
		self.x = self.dataframe.iloc[:-1,:-1]#dataframeの最後のcol以外を説明変数としている
		#print(self.x)
		self.y = self.dataframe.iloc[:-1,[-1]]#最後のcolはランクラベル
		#print(self.y)
	def test_extractor(self):
		self.x_test = self.dataframe.iloc[-1,:-1]#dataframeの最後を抜き出す
		self.y_test = self.dataframe.iloc[-1,-1]
		#print(self.x_test)
	def grid(self):#ハイパーパラメタの決定
		tuned_parameters=[{"C":[0.85,1,5,10],"kernel":[self.kernel],"gamma":[1,0.1,0.001,0.0001]}]
		self.classifier = GridSearchCV(
			SVC(),
			tuned_parameters,
			cv=2,
		)
		self.classifier.fit(self.x,self.y.values.reshape(-1,))
		#print(self.classifier.cv_results_)
		#print(self.classifier.best_params_)
		self.C = self.classifier.best_params_['C']
		self.kernel=self.classifier.best_params_['kernel']
		self.gamma=self.classifier.best_params_['gamma']
	def generator(self):
		self.teature_extractor()
		self.test_extractor()
		self.grid()
		self.clf = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
		return self.clf
		
def main():
	df=rank_train_data
	print(df)
	rank = gen_model(df)
	clf = rank.generator()
	with open('model.pickle','wb') as f:
		pickle.dump(clf,f)
main()
'
, @input_data_1 = N'select "season_id", "kill_death_ratio", "average_damage", "match_counts", "is_party", "rank_id",  from [RankPrediction].[ml_predict].[prediction_data] '
    , @input_data_1_name = N'rank_train_data'
    , @params = N'@trained_model varbinary(max) OUTPUT'
    , @trained_model = @trained_model OUTPUT;
END;
GO

TRUNCATE TABLE rank_py_models;

DECLARE @model VARBINARY(MAX);
EXEC generate_rank_py_model @model OUTPUT;

INSERT INTO rank_py_models (model_name, model) VALUES('rbf_model', @model);

SELECT * FROM rank_py_models;

DROP PROCEDURE IF EXISTS py_predict_rentalcount;
GO
CREATE PROCEDURE py_predict_rentalcount (@model varchar(100))
AS
BEGIN
    DECLARE @py_model varbinary(max) = (select model from rental_py_models where model_name = @model);

    EXEC sp_execute_external_script 
                    @language = N'Python'
                  , @script = N'
import numpy as np
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
	def __init__(self, dataframe):
		super(mlforrank, self).__init__()
		self.dataframe = dataframe
		self.limit = 0.8
	def teature_extractor(self):
		self.x = self.dataframe.iloc[:-1,:-1]#dataframeの最後のcol以外を説明変数としている
		#print(self.x)
		self.y = self.dataframe.iloc[:-1,[-1]]#最後のcolはランクラベル
		#print(self.y)
	def test_extractor(self):
		self.x_test = self.dataframe.iloc[-1,:-1]#dataframeの最後を抜き出す
		self.y_test = self.dataframe.iloc[-1,-1]
		#print(self.x_test)
	def cross_val(self):#cross validationのスコアを返す
		skf = StratifiedKFold(shuffle=True, random_state=0, n_splits=3)
		scores = cross_validate(self.clf,self.x,self.y.values.reshape(-1,), cv= skf, return_train_score=True)
		#print(scores)
		print(np.mean(scores["test_score"]))
		return np.mean(scores["test_score"])
	def estimator(self):
		self.teature_extractor()
		self.test_extractor()
		with open('model2.pickle','rb') as f:
			self.clf = pickle.load(f)
		self.clf.fit(self.x,self.y.values.reshape(-1,))
		score = self.cross_val()#正答率のようなもの
		if score < self.limit :
			return self.y_test
		else:
			pred_y = self.classifier.predict(self.x_test.values.reshape([1,-1]))
			return pred_y[0]
		
def estimate():
	df=rank_score_data
	print(df)
	rank = mlforrank(df)
	print(rank.estimator())

estimate()
'
    , @input_data_1 = N'Select "season_id", "kill_death_ratio", "average_damage", "match_counts", "is_party", "rank_id",  from [RankPrediction].[ml_predict].[prediction_data] '
    , @input_data_1_name = N'rank_score_data'
    , @params = N'@py_model varbinary(max)'
    , @py_model = @py_model
    with result sets (("rank_Predicted" int, "rank" float, "season_id" float,"kill_death_ratio" float,"average_damage" float,"match_counts" float,"is_party" float,));
END;
GO