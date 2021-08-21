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
	grid search (cross_validation)でハイパーパラメータ達を決める
	正答率はmodelを作ったり読み込んだりした後に出す
	教師データを分割し未知データのほうの正答率がある程度いくまでは例の手順
	self.classifierはgrid search 用
	self.clfは答えを返す用
	grid_enebleがTrueならパラメータを探してからモデルを作る
	Falseなら保存してあるモデルを読み込む
	"""
	def __init__(self, dataframe, id, grid_eneble):
		super(mlforrank, self).__init__()
		self.dataframe = dataframe
		self.id = id #入力したユーザーのid
		self.grid_enable = grid_eneble
		self.limit = 0.8 #正答率がこれ以下なら入ってきたまま返す
		self.C = 0.85
		self.kernel = 'rbf'
		self.gamma = 0.01
	def teature_extractor(self):
		self.x = self.dataframe[self.dataframe['id'] != self.id].iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
		self.y = self.dataframe[self.dataframe['id'] != self.id].iloc[:,[-1]]#最後のcolはランクラベル
		print(self.y)
	def test_extractor(self):
		self.x_test = self.dataframe[self.dataframe['id'] == self.id].iloc[-1,1:-1]#dataframeの最後を抜き出す
		self.y_test = self.dataframe[self.dataframe['id'] == self.id].iloc[-1,-1]
		print(self.x_test)
	def cross_val(self):#cross validationのスコアを返す
		skf = StratifiedKFold(shuffle=True, random_state=0, n_splits=3)
		scores = cross_validate(self.clf,self.x,self.y.values.reshape(-1,), cv= skf, return_train_score=True)
		#print(scores)
		print(np.mean(scores["test_score"]))
		return np.mean(scores["test_score"])
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
	def weight(self,unestimation):
		d = [(-1, 98), (0, 844), (1, 1028)]
		a, w = zip(*d)
		print(a, w)
		# -> ('foo', 'bar', 'baz') (98, 844, 1028)

		w2 = np.array(w) / sum(w)
		v = np.random.choice(a, p=w2)
		"""
		print(v)
		from collections import Counter
		c = [ np.random.choice(a, p=w2) for i in range(sum(w)) ]
		print(Counter(c))
		"""
		return unestimation+v
		
	def estimator(self):
		self.teature_extractor()
		self.test_extractor()
		if self.grid_enable:
			self.grid()
			self.clf = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
			with open('model.pickle','wb') as f:
				pickle.dump(self.clf,f)
		else:
			with open('model.pickle','rb') as f:
				self.clf = pickle.load(f)
		self.clf.fit(self.x,self.y.values.reshape(-1,))
		score = self.cross_val()#正答率のようなもの
		if score < self.limit :
			return self.weight(self.y_test)#重みのついたランダムを返す
		else:
			pred_y = self.classifier.predict(self.x_test.values.reshape([1,-1]))
			return pred_y[0]
		
def main():
	df=pd.read_csv(r'Machine_Learning\test.csv')
	print(df)
	rank = mlforrank(df, 2,True)
	print(rank.estimator())
if __name__ == '__main__':
	main()
		