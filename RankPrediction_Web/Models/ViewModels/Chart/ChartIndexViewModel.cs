using System;
using RankPrediction_Web.Models.DbContexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RankPrediction_Web.Models.ViewModels.Chart
{
    public class ChartIndexViewModel : LayoutViewModel
    {
        public ChartIndexViewModel(RankPredictionContext db) : base()
        {

            RankAverageKillRatio = new ChartData()
            {
                ChartLabel = "ランク別平均キルレシオ",
                //ここでデータ取得処理
                ChartValues =
                   db.PredictionData
                   .GroupBy(item => new { item.RankId, item.Rank.RankNameJa })
                   .OrderBy(item => item.Key.RankId)
                   .Select(item => new ChartValues()
                   {
                       Label = item.Key.RankNameJa,
                       Value = item.Average(elem => elem.KillDeathRatio.Value)
                   })
                   .ToList()
            };

        }

        /// <summary>
        /// ランクと平均キルレシオのチャートデータクラス
        /// </summary>
        public ChartData RankAverageKillRatio { get; set; }


    }
}
