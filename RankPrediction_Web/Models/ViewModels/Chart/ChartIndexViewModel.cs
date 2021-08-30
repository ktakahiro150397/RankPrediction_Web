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

            RankAverageKillRatio = new ChartData();

            var rankAverageKills =
               db.PredictionData
               .GroupBy(item => item.RankId)
               .OrderBy(item => item.Key)
               .Select(item => new { RankId = item.Key, KillRatioAverage = item.Average(elem => elem.KillDeathRatio) })
               .ToList();


            foreach(var elem in rankAverageKills)
            {
                RankAverageKillRatio.data.Add(elem.KillRatioAverage.Value);
            }

        }

        /// <summary>
        /// ランクと平均キルレシオのチャートデータクラス
        /// </summary>
        public ChartData RankAverageKillRatio { get; set; }


    }
}
