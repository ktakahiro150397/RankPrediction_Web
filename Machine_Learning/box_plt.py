import matplotlib.pyplot as plt
import numpy as np
import pandas as pd

def plotter(df, key):
    boxplot = df.boxplot(column=[key], by="rank_id")
    boxplot.plot()
    

def flier(df, key):
    q1 = df[key].quantile(0.001)
    q2 = df[key].quantile(0.975)
    max = q2
    min = q1
    df = df[(df[key] < max)]
    df = df[(df[key] > min)]
    return df

def main():
    df=pd.read_csv(r'Machine_Learning\1114.csv')
    keylist=['average_damage','kill_death_ratio', 'match_counts']
    for i in keylist:
        df = flier(df, i)
        plotter(df, i)
    plt.show()
if __name__ == '__main__':
	main()
