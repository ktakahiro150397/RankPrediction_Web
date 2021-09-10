using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RankPrediction_Web.Models.DbContexts;

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

        private IChartData GetRankToAverageKillRatio()
        {
            throw new NotImplementedException();
        }

        private IChartData GetRankToAverageDamage()
        {
            throw new NotImplementedException();
        }

        private IChartData GetRankToAverageMatchCount()
        {
            throw new NotImplementedException();
        }


    }
}
