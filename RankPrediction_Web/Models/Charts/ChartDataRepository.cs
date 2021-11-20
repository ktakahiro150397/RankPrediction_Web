using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RankPrediction_Web.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using RankPrediction_Web.Extensions;

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
                case ChartDisplayData.RankToMedianKillRatio:
                    return GetRankToMedianKillRatio();
                case ChartDisplayData.RankToMedianDamage:
                    return GetRankToMedianDamage();
                case ChartDisplayData.RankToMedianMatchCount:
                    return GetRankToMedianMatchCount();
                default:
                    return null;
            }
        }

        /// <summary>
        /// ランクに対するキルレシオの中央値データを返します。
        /// </summary>
        /// <returns></returns>
        private IChartData GetRankToMedianKillRatio()
        {
            //ランクに対するキルレシオデータの取得
            var rankToAveKillRatio =
                _db
                .PredictionData
                .Join(
                    _db.Ranks,
                    pred => pred.RankId,
                    rank => rank.RankId,
                    (pred,rank) => new
                    {
                        RankId = rank.RankId,
                        RankName = rank.RankNameJa,
                        KillDeathRatio = pred.KillDeathRatio
                    })
                .AsEnumerable()
                .OrderBy(item => item.RankId)
                .GroupBy(item => new { item.RankId, item.RankName })
                .Select(item => new
                {
                    RankLabel = item.Key.RankName,
                    Value = item.Median(elem => elem.KillDeathRatio)
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

        /// <summary>
        /// ランクに対するダメージの中央値データを返します。
        /// </summary>
        /// <returns></returns>
        private IChartData GetRankToMedianDamage()
        {
            //ランクに対するダメージデータの取得
            var rankToAveKillRatio =
                _db
                .PredictionData
                .Join(
                    _db.Ranks,
                    pred => pred.RankId,
                    rank => rank.RankId,
                    (pred, rank) => new
                    {
                        RankId = rank.RankId,
                        RankName = rank.RankNameJa,
                        AverageDamage = pred.AverageDamage
                    })
                .AsEnumerable()
                .OrderBy(item => item.RankId)
                .GroupBy(item => new { item.RankId, item.RankName })
                .Select(item => new
                {
                    RankLabel = item.Key.RankName,
                    Value = item.Median(elem => elem.AverageDamage)
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

        /// <summary>
        /// ランクに対するマッチ数の中央値データを返します。
        /// </summary>
        /// <returns></returns>
        private IChartData GetRankToMedianMatchCount()
        {
            //ランクに対するマッチ数データの取得
            var rankToAveKillRatio =
                _db
                .PredictionData
                .Where(item => item.MatchCounts != -1)
                .Join(
                    _db.Ranks,
                    pred => pred.RankId,
                    rank => rank.RankId,
                    (pred, rank) => new
                    {
                        RankId = rank.RankId,
                        RankName = rank.RankNameJa,
                        MatchCounts = pred.MatchCounts
                    })
                .AsEnumerable()
                .OrderBy(item => item.RankId)
                .GroupBy(item => new { item.RankId, item.RankName })
                .Select(item => new
                {
                    RankLabel = item.Key.RankName,
                    Value = item.Median(elem => elem.MatchCounts)
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


    }
}
