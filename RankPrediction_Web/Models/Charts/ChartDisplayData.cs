using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankPrediction_Web.Models.Charts
{
    
    /// <summary>
    /// チャートで表示するデータを表します。
    /// </summary>
    public enum ChartDisplayData
    {
        /// <summary>
        /// ランクに対する平均キルレシオ
        /// </summary>
        RankToAverageKillRatio = 1,

        /// <summary>
        /// ランクに対する平均ダメージ
        /// </summary>
        RankToAverageDamage = 2,

        /// <summary>
        /// ランクに対する平均ゲーム数
        /// </summary>
        RankToAverageMatchCount = 3,

    }

    /// <summary>
    /// チャートの種類を表します。
    /// </summary>
    public enum ChartType
    {
        bar = 1
    }


}
