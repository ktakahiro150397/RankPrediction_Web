import numpy as np
from numpy.lib.twodim_base import triu_indices_from
import pandas as pd
from sklearn.metrics import accuracy_score
from sklearn.multiclass import OneVsRestClassifier
from sklearn.svm import SVC
from sklearn.metrics import hinge_loss
from sklearn.model_selection import cross_validate, StratifiedKFold, GridSearchCV
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
		self.x = self.dataframe.iloc[:-1,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
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
	df=pd.read_csv(r'Machine_Learning\test.csv')
	print(df)
	rank = gen_model(df)
	clf = rank.generator()
	with open('model2.pickle','wb') as f:
		pickle.dump(clf,f)
main()
		