"""
入力するものとして以下の３つが必要
#データ
#予測するデータのid
#modelをつくるかの true false
"""

import pickle

import numpy as np
import pandas as pd
from sklearn.model_selection import (GridSearchCV, StratifiedKFold,
                                     cross_validate)
from sklearn.preprocessing import MinMaxScaler, StandardScaler
from sklearn.svm import SVC


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
	def __init__(self, dataframe, id):
		super(mlforrank, self).__init__()
		self.dataframe = dataframe
		self.id = id #入力したユーザーのid
		self.q1 = 0.04 #下側の外れ値をどれだけ省くか
		self.q2 = 0.92 #上側の外れ値をどれだけ省くか。上位 (1-q2)*100 % のデータを外れ値として省く
		rank_id_list=self.dataframe["rank_id"].unique()
		for i in self.dataframe.columns.values[1:-2]:
			for rank in rank_id_list:
				self.flier(i, rank)
		self.standardizatoin_normalization()
		self.limit = 0.1 #正答率がこれ以下なら入ってきたまま返す
		self.nsplit = 2 #cvとかgridとかでデータをスプリットする個数
		self.C = 10000
		self.kernel = "rbf"
		self.gamma = "scale"
		self.userrank = self.dataframe[self.dataframe["id"] == self.id].iloc[-1,-1]#計算するidのランク
		self.norank = self.userrank == 22
		self.rank_col_name = self.dataframe.columns.values[-1]#ランクが入っているcolの名前
		print("rank conuts")
		print(self.dataframe[self.rank_col_name].value_counts())
		self.number_of_members_in_each_rank = self.dataframe[self.rank_col_name].value_counts().min()#データベースに入っているランクの中で一番少ないやつの頻度
		self.do_grid = self.number_of_members_in_each_rank > self.nsplit + 1 #gridするか否か.T/F.+している数値はサンプル数に応じ変更する
		self.do_split = self.number_of_members_in_each_rank > self.nsplit
		self.do_est = self.number_of_members_in_each_rank >= self.nsplit
		self.dfbool = self.dataframe[self.dataframe[self.rank_col_name] == self.userrank]#計算するidのランクと一致するrowのみで構成されるdf
		self.unique = len(self.dfbool)<=1 #計算するidのランクがそいつだけしか無いか.T/F
		print(self.unique)
	def standardizatoin_normalization(self):
		sc = StandardScaler()
		new_cols = [col for col in self.dataframe.columns if not (col == "id" or col == "rank_id" or col == "is_party")]
		def_cols = [col for col in self.dataframe.columns if col == "id" or col == "rank_id" or col == "is_party"]
		sc.fit(self.dataframe[new_cols][self.dataframe["id"] != self.id])
		std = sc.transform(self.dataframe[new_cols])
		dfa = pd.DataFrame(std, columns=new_cols)
		dfb = pd.concat([self.dataframe[def_cols].reset_index(drop=True), dfa], axis = 1)
		self.dataframe = dfb.reindex(columns = self.dataframe.columns)
		print(self.dataframe.min())
	def teature_extractor(self):
		print("外れ値削除後")
		print(self.dataframe)
		self.x = self.dataframe[self.dataframe["id"] != self.id].iloc[:,1:-1]#dataframeの最後のcol以外を説明変数としている
		self.y = self.dataframe[self.dataframe["id"] != self.id].iloc[:,[-1]]#最後のcolはランクラベル
	def flier(self,key, rank):#指定id以外において外れ値削除
		dfa = self.dataframe[self.dataframe["rank_id"]==rank]#外れ値はランクごとに計算する
		q1 = dfa[key].quantile(self.q1)
		q2 = dfa[key].quantile(self.q2)
		max = q2
		min = q1
		self.dataframe = self.dataframe[(self.dataframe["id"] == self.id) | (self.dataframe["rank_id"]!=rank) | ((self.dataframe[key] <= max) & (self.dataframe["rank_id"]==rank))]
		self.dataframe = self.dataframe[(self.dataframe["id"] == self.id) | (self.dataframe["rank_id"]!=rank) | ((self.dataframe[key] >= min) & (self.dataframe["rank_id"]==rank))]
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
	def fitting_score(self):#モデルで教師データを学習した時のスコアを返す
		return self.clf.score(self.x, self.y)
	def grid(self):#ハイパーパラメタの決定
		tuned_parameters=[{"C":[0.85,1,10,100,1000,10000],"kernel":[self.kernel],"gamma":["scale",1,0.1,0.001,0.0001]}]
		self.classifier = GridSearchCV(
			SVC(),
			tuned_parameters,
			cv=self.nsplit,
			scoring='neg_mean_absolute_error'
		)
		self.classifier.fit(self.x,self.y.values.reshape(-1,))
		#print(self.classifier.cv_results_)
		#print(self.classifier.best_params_)
		self.C = self.classifier.best_params_['C']
		self.kernel=self.classifier.best_params_['kernel']
		self.gamma=self.classifier.best_params_['gamma']
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
		if self.unique and not self.norank:#ユニークでランクを入力されているやつ
			print(self.unique)
			return self.weight(self.userrank)#重みのついたランダムを返す
		self.test_extractor()
		self.teature_extractor()
		if self.norank:	
			with open('model.pickle','rb') as f:
				self.clf = pickle.load(f)
			self.clf.fit(self.x,self.y.values.reshape(-1,))
			pred_y = self.clf.predict(self.x_test.values.reshape([1,-1]))
			print("predicted!!!!!")
			score = self.fitting_score()
			print(score)
			return pred_y[0]

		if self.do_grid:
			print("grid enable")
			self.grid()
			self.clf = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
			print(self.C, self.kernel, self.gamma)
			with open('model.pickle','wb') as f:
				pickle.dump(self.clf,f)
		else:
			print("grid disable")
			self.clf = SVC(C=self.C, kernel=self.kernel, gamma=self.gamma)
		self.clf.fit(self.x,self.y.values.reshape(-1,))

		if self.do_split:
			print("cv enable")
			score = self.cross_val()#正答率のようなもの
		elif self.do_est:
			score = self.fitting_score()
		else :
			return self.weight(self.userrank)
		if score < self.limit :
			return self.weight(self.userrank)#重みのついたランダムを返す
		else:
			pred_y = self.clf.predict(self.x_test.values.reshape([1,-1]))
			all_pred = self.clf.predict(self.x).astype(np.int8)
			self.y = self.y.astype(np.int8)
			abs_diff = [np.abs(n-m) for n,m in zip(all_pred, self.y.values.reshape(-1,).astype(np.int8))]
			print("predicted")
			print("score: {}".format(score))
			print("ave diff:{}".format(np.average(abs_diff)))
			return pred_y[0]

def main():
	df=pd.read_csv(r'Machine_Learning\1114.csv', )
	print(df)
	rank = mlforrank(df, 9)
	print(rank.estimator())
if __name__ == '__main__':
	main()