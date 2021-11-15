using RankPrediction_Web.Models.Charts;
using System.Collections.Generic;

namespace RankPrediction_Web.Models.ViewModels.Chart
{
    /// <summary>
    /// チャート表示画面のビューモデル。
    /// </summary>
    public class RankToChartViewModel : LayoutViewModel
    {
        public RankToChartViewModel():base()
        {
            DisplayChartDataList = new Dictionary<ChartDisplayData, string>
            {
                {ChartDisplayData.RankToMedianDamage,"平均ダメージの中央値" },
                {ChartDisplayData.RankToMedianKillRatio,"キルレシオの中央値" },
                {ChartDisplayData.RankToMedianMatchCount,"マッチ数の中央値" },
            };
        }
            
        /// <summary>
        /// チャート選択ドロップダウンに割り当てるデータ。
        /// </summary>
        public Dictionary<ChartDisplayData, string> DisplayChartDataList { get; }

    }
}
