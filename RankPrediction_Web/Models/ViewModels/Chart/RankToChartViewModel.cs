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
                {ChartDisplayData.RankToAverageDamage,"平均ダメージ" },
                {ChartDisplayData.RankToAverageKillRatio,"平均キルレシオ" },
                {ChartDisplayData.RankToAverageMatchCount,"平均マッチ数" },
            };
        }

        /// <summary>
        /// チャート選択ドロップダウンに割り当てるデータ。
        /// </summary>
        public Dictionary<ChartDisplayData, string> DisplayChartDataList { get; }

    }
}
