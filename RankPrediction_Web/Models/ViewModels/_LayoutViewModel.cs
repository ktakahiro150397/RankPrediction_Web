using System;
using RankPrediction_Web.Models.SnsShare;

namespace RankPrediction_Web.Models.ViewModels
{
    /// <summary>
    /// レイアウトページのビューモデルを兼ねる基底クラス。
    /// </summary>
    public class LayoutViewModel
    {
        public LayoutViewModel()
        {
            SnsShare = new SnsShareModel("AIでAPEXの実力を診断してみよう！", "秘められた実力が分かるかも！");
            SnsShare.Twitter.HashTags.Add("APEX実力診断");
            SnsShare.Twitter.HashTags.Add("ApexLegends");
        }

        /// <summary>
        /// SNS共有に使用するデータを表します。
        /// </summary>
        public SnsShareModel SnsShare { get; set; }

    }
}
