"""
入力するものとして以下の３つが必要
#データ
#予測するデータのid
#modelをつくるかの true false
"""

import numpy as np
import pandas as pd
from sklearn.svm import SVC
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
<<<<<<< HEAD
	def __init__(self, dataframe, id):
		super(mlforrank, self).__init__()
		self.dataframe = dataframe
		self.id = id #入力したユーザーのid
		self.limit = 0.8 #正答率がこれ以下なら入ってきたまま返す
		self.nsplit = 3 #cvとかgridとかでデータをスプリットする個数
=======
	def __init__(self, dataframe, id, grid_eneble):
		super(mlforrank, self).__init__()
		self.dataframe = dataframe
		self.id = id #入力したユーザーのid
		self.grid_enable = grid_eneble
		self.limit = 0.8 #正答率がこれ以下なら入ってきたまま返す
>>>>>>> 0f29b05c3d9d820bf776e891b715ada3a10316d2
		self.C = 0.85
		self.kernel = "rbf"
		self.gamma = 0.01
		self.userrank = self.dataframe[self.dataframe["id"] == self.id].iloc[-1,-1]#計算するidのランク
		self.rank_col_name = self.dataframe.columns.values[-1]#ランクが入っているcolの名前
		print("rank conuts")
		print(self.dataframe[self.rank_col_name].value_counts())
		self.number_of_members_in_each_rank = self.dataframe[self.rank_col_name].value_counts().min()#データベースに入っているランクの中で一番少ないやつの頻度
		self.do_grid = self.number_of_members_in_each_rank > self.nsplit + 10 #gridするか否か.T/F.+している数値はサンプル数に応じ変更する
		self.do_split = self.number_of_members_in_each_rank > self.nsplit
		self.do_est = self.number_of_members_in_each_rank >= self.nsplit
		self.dfbool = self.dataframe[self.dataframe[self.rank_col_name] == self.userrank]#計算するidのランクと一致するrowのみで構成されるdf
		self.unique = len(self.dfbool)<=1 #計算するidのランクがそいつだけしか無いか.T/F
		print(self.unique)
	def teature_extractor(self):
<<<<<<< HEAD
		self.x = self.dataframe[self.dataframe["id"] != self.id].iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		print(self.x)
		self.y = self.dataframe[self.dataframe["id"] != self.id].iloc[:,[-1]]#最後のcolはランクラベル
		print(self.y)
	def test_extractor(self):
		self.x_test = self.dataframe[self.dataframe["id"] == self.id].iloc[-1,1:-1]#dataframeの最後を抜き出す
		self.y_test = self.userrank
	def cross_val(self):#cross validationのスコアを返す
		skf = StratifiedKFold(shuffle=True, random_state=0, n_splits=self.nsplit)
=======
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
>>>>>>> 0f29b05c3d9d820bf776e891b715ada3a10316d2
		scores = cross_validate(self.clf,self.x,self.y.values.reshape(-1,), cv= skf, return_train_score=True)
		#print(scores)
		print(np.mean(scores["test_score"]))
		return np.mean(scores["test_score"])
	def fitting_score(self):#モデルで教師データを学習した時のスコアを返す
		return self.clf.score(self.x, self.y)
	def grid(self):#ハイパーパラメタの決定
		tuned_parameters=[{"C":[0.85,1,5,10],"kernel":[self.kernel],"gamma":[1,0.1,0.001,0.0001]}]
		self.classifier = GridSearchCV(
			SVC(),
			tuned_parameters,
			cv=self.nsplit,
		)
		self.classifier.fit(self.x,self.y.values.reshape(-1,))
		#print(self.classifier.cv_results_)
		#print(self.classifier.best_params_)
		self.C = self.classifier.best_params_['C']
		self.kernel=self.classifier.best_params_['kernel']
		self.gamma=self.classifier.best_params_['gamma']
	def weight(self,unestimation):
<<<<<<< HEAD
		d = [(0, 98), (1, 844), (2, 1028)]
=======
		d = [(-1, 98), (0, 844), (1, 1028)]
>>>>>>> 0f29b05c3d9d820bf776e891b715ada3a10316d2
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
<<<<<<< HEAD
		if result>=22:
			result = 21
		elif result<=-1:
=======
		if result==22:
			result = 21
		elif result==-1:
>>>>>>> 0f29b05c3d9d820bf776e891b715ada3a10316d2
			result = 0
		return result
		
	def estimator(self):
<<<<<<< HEAD
		if self.unique:
			print(self.unique)
			return self.weight(self.userrank)#重みのついたランダムを返す
		else:
			self.teature_extractor()
			self.test_extractor()
			if self.do_grid:
				self.grid()
				self.clf = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
				with open('model.pickle','wb') as f:
					pickle.dump(self.clf,f)
			else:
				with open('model.pickle','rb') as f:
					self.clf = pickle.load(f)
			self.clf.fit(self.x,self.y.values.reshape(-1,))
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
=======
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
>>>>>>> 0f29b05c3d9d820bf776e891b715ada3a10316d2
		
def main():
	df=pd.read_csv(r'Machine_Learning\test.csv')
	print(df)
<<<<<<< HEAD
	rank = mlforrank(df, 6)
=======
	rank = mlforrank(df, 2,True)
>>>>>>> 0f29b05c3d9d820bf776e891b715ada3a10316d2
	print(rank.estimator())
if __name__ == '__main__':
	main()