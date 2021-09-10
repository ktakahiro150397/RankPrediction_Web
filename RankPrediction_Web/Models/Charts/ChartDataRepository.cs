using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RankPrediction_Web.Models.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace RankPrediction_Web.Models.Charts
{

    /// <summary>
    /// チャートに表示するデータを提供します。
    /// </summary>
    public class ChartDataRepository
    {

        private RankPredictionContext _db;

        public ChartDataRepository(RankPredictionContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 指定のチャートに適したチャートデータを返します。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IChartData RetrieveChartDataByChartType(ChartDisplayData type)
        {

            switch (type)
            {
                case ChartDisplayData.RankToAverageKillRatio:
                    return GetRankToAverageKillRatio();
                case ChartDisplayData.RankToAverageDamage:
                    return GetRankToAverageDamage();
                case ChartDisplayData.RankToAverageMatchCount:
                    return GetRankToAverageMatchCount();
                default:
                    return null;
            }
        }

        /// <summary>
        /// ランクに対するキルレシオのデータを返します。
        /// </summary>
        /// <returns></returns>
        private IChartData GetRankToAverageKillRatio()
        {
            //ランクに対する平均キルレシオデータの取得
            var rankToAveKillRatio =
                _db
                .PredictionData
                .GroupBy(item => new { item.RankId, item.Rank.RankNameJa })
                .Select(item => new
                {
                    RankLabel = item.Key.RankNameJa,
                    Value = item.Average(elem => elem.KillDeathRatio)
                })
                .ToList();

            //返却データの生成
            var retData = new ChartJsData();

            retData.Config.ChartTypeValue = ChartType.bar;

            //ラベルを設定
            retData.Config.Data.Labels = rankToAveKillRatio.Select(item => item.RankLabel).ToList();

            //データを設定
            retData.Config.Data.DataSets.Add(
                new DataSetItem()
                {
                    Data = rankToAveKillRatio.Select(item => item.Value.Value).ToList()
                }
            );

            return retData;

        }

        private IChartData GetRankToAverageDamage()
        {
            //ランクに対する平均ダメージデータの取得
            var rankToAveKillRatio =
                _db
                .PredictionData
                .GroupBy(item => new { item.RankId, item.Rank.RankNameJa })
                .Select(item => new
                {
                    RankLabel = item.Key.RankNameJa,
                    Value = item.Average(elem => elem.AverageDamage)
                })
                .ToList();

            //返却データの生成
            var retData = new ChartJsData();

            retData.Config.ChartTypeValue = ChartType.bar;

            //ラベルを設定
            retData.Config.Data.Labels = rankToAveKillRatio.Select(item => item.RankLabel).ToList();

            //データを設定
            retData.Config.Data.DataSets.Add(
                new DataSetItem()
                {
                    Data = rankToAveKillRatio.Select(item => item.Value.Value).ToList()
                }
            );

            return retData;
        }

        private IChartData GetRankToAverageMatchCount()
        {
            //ランクに対する平均マッチ数データの取得
            var rankToAveKillRatio =
                _db
                .PredictionData
                .Where(item => item.MatchCounts != -1)
                .GroupBy(item => new { item.RankId, item.Rank.RankNameJa })
                .Select(item => new
                {
                    RankLabel = item.Key.RankNameJa,
                    Value = item.Average(elem => elem.MatchCounts)
                })
                .ToList();

            //返却データの生成
            var retData = new ChartJsData();

            retData.Config.ChartTypeValue = ChartType.bar;

            //ラベルを設定
            retData.Config.Data.Labels = rankToAveKillRatio.Select(item => item.RankLabel).ToList();

            //データを設定
            retData.Config.Data.DataSets.Add(
                new DataSetItem()
                {
                    Data = (IList<double>)rankToAveKillRatio.Select(item => item.Value.Value).ToList()
                }
            );

            return retData;
        }


    }
}
