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
		self.kernel = "rbf"
		self.gamma = 0.01
	def teature_extractor(self):
		self.x = self.dataframe.iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
		self.y = self.dataframe.iloc[:,[-1]]#最後のcolはランクラベル
		print(self.y)
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
		self.C = self.classifier.best_params_["C"]
		self.kernel=self.classifier.best_params_["kernel"]
		self.gamma=self.classifier.best_params_["gamma"]
	def generator(self):
		self.teature_extractor()
		self.grid()
		self.clf = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
		self.clf.fit(self.x,self.y.values.reshape(-1,))
		return self.clf
		

df=rank_train_data
print(df)
rank = gen_model(df)
clf = rank.generator()
trained_model = pickle.dumps(clf)
'
	, @input_data_1 = N'select "id", "kill_death_ratio", "average_damage", "match_counts", "is_party", "rank_id"  from [RankPrediction].[ml_predict].[prediction_data] '
    , @input_data_1_name = N'rank_train_data'
    , @params = N'@trained_model varbinary(max) OUTPUT'
    , @trained_model = @trained_model OUTPUT;
END;
GO