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
            SnsShare = new SnsShareModel("Apex 実力診断サイト", "ここが本文");
            SnsShare.Twitter.HashTags.Add("test");
            SnsShare.Twitter.HashTags.Add("hash");
            SnsShare.Twitter.HashTags.Add("tags");
        }

        /// <summary>
        /// SNS共有に使用するデータを表します。
        /// </summary>
        public SnsShareModel SnsShare { get; set; }

    }
}
