import matplotlib.pyplot as plt
import numpy as np
import pandas as pd

def plotter(df, key):
    boxplot = df.boxplot(column=[key], by="rank_id")
    boxplot.plot()
    

def flier(df, rank, key):
    dfa = df[df["rank_id"]==rank]
    q1 = dfa[key].quantile(0.02)
    q2 = dfa[key].quantile(0.90)
    max = q2
    min = q1
    print(key,rank)
    print("max:{}".format(max))
    print("min:{}".format(min))
    df = df[(df["rank_id"]!=rank) | ((df[key] <= max) & (df["rank_id"]==rank))]
    df = df[(df["rank_id"]!=rank) | ((df[key] >= min) & (df["rank_id"]==rank))]
    return df

def main():
    df=pd.read_csv(r'Machine_Learning\1114.csv')
    keylist=['average_damage','kill_death_ratio', 'match_counts']
    rank_id_list=df['rank_id'].unique()
    print(rank_id_list)
    for i in keylist:
        for rank in rank_id_list:
            df = flier(df, rank, i)
        plotter(df, i)
    plt.show()
if __name__ == '__main__':
	main()
