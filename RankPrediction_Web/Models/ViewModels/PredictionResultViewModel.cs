using System;
using RankPrediction_Web.Models.DbContexts;
using RankPrediction_Web.Models.SnsShare;

namespace RankPrediction_Web.Models.ViewModels
{
    public class PredictionResultViewModel : LayoutViewModel
    {
        private int _id { get; set; }


        public PredictionResult PredictedResult { get; set; }

        /// <summary>
        /// 指定されたIDの結果を表示するViewModelを初期化します。
        /// </summary>
        /// <param name="id"></param>
        public PredictionResultViewModel(RankPredictionContext dbContext, int id)
        {
            _id = id;

            //対象IDの予測結果を取得する
            PredictedResult = new PredictionResult(dbContext, _id);

            if(PredictedResult != null)
            {
                //SNS共有は結果に応じたカスタム文言に修正する
                SnsShare = new SnsShareModel("title", "shareText");
            }

        }

    }
}
