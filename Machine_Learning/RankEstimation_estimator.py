import numpy as np
from numpy.lib.twodim_base import triu_indices_from
import pandas as pd
from sklearn.metrics import accuracy_score
from sklearn.multiclass import OneVsRestClassifier
from sklearn.svm import SVC
from sklearn.metrics import hinge_loss
from sklearn.model_selection import cross_validate, StratifiedKFold, GridSearchCV
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
	df=pd.read_csv(r'Machine_Learning\test.csv')
	print(df)
	rank = mlforrank(df)
	print(rank.estimator())

estimate()
		