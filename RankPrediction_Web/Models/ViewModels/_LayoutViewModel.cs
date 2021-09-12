using System;
using RankPrediction_Web.Models.SnsShare;
using RankPrediction_Web.Models.Authentication;

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

            
            User = new AuthenticatedUser();
        }

        /// <summary>
        /// SNS共有に使用するデータを表します。
        /// </summary>
        public SnsShareModel SnsShare { get; set; }

        /// <summary>
        /// ユーザーの情報を表します。
        /// </summary>
        public AuthenticatedUser User { get; set; }


    }
}
