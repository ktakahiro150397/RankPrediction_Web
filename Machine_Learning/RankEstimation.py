import numpy as np
from numpy.lib.twodim_base import triu_indices_from
import pandas as pd
from sklearn.metrics import accuracy_score
from sklearn.multiclass import OneVsRestClassifier
from sklearn.svm import SVC
from sklearn.metrics import hinge_loss
from sklearn.model_selection import cross_validate, StratifiedKFold, GridSearchCV

class mlforrank(object):
	"""
	多クラス分類でヒンジロスを出すのは無理ポイ。できるかも知らんけどsklearnのhinge_lossで簡単にだせるものじゃなさげ
	cross_validationでC決めたい
	正答率はとりあえずだす。
	GridS
	教師データ分割したときの未知データのほうの正答率がある程度いくまでは
	"""
	def __init__(self, dataframe):
		super(mlforrank, self).__init__()
		self.dataframe = dataframe
		self.limit = 0.7 #正答率がこれ以下なら入ってきたまま返す
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
	def cross_val(self):#cross validationのスコアを返す
		skf = StratifiedKFold(shuffle=True, random_state=0, n_splits=3)
		scores = cross_validate(self.classifier,self.x,self.y.values.reshape(-1,), cv= skf, return_train_score=True)
		print(scores)
		print(np.mean(scores["test_score"]))
		return np.mean(scores["test_score"])
	def grid(self):#ハイパーパラメタの決定
		tuned_parameters=[{"C":[0.85,1,5,10],"kernel":[self.kernel],"gamma":[1,0.1,0.001,0.0001]}]
		clf = GridSearchCV(
			SVC(),
			tuned_parameters,
			cv=2,
		)
		clf.fit(self.x,self.y.values.reshape(-1,))
		print(clf.predict(self.x))
		print(clf.cv_results_)
		print(clf.best_params_)
	def estimator(self):
		self.teature_extractor()
		self.test_extractor()
		self.grid()
		self.classifier = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
		self.classifier.fit(self.x,self.y.values.reshape(-1,))
		score = self.cross_val()
		
		if score < self.limit :
			return self.y_test
		else:
			pred_y = self.classifier.predict(self.x_test.values.reshape([1,-1]))
			return pred_y
		
def main():
	df=pd.read_csv(r'Machine_Learning\test.csv')
	print(df)
	rank = mlforrank(df)
	print(rank.estimator())
if __name__ == '__main__':
	main()
		