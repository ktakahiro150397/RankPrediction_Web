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
        /// ランクに対するキルレシオの中央値
        /// </summary>
        RankToMedianKillRatio = 1,

        /// <summary>
        /// ランクに対するダメージの中央値
        /// </summary>
        RankToMedianDamage = 2,

        /// <summary>
        /// ランクに対するゲーム数の中央値
        /// </summary>
        RankToMedianMatchCount = 3,

    }

    /// <summary>
    /// チャートの種類を表します。
    /// </summary>
    public enum ChartType
    {
        bar = 1
    }


}
