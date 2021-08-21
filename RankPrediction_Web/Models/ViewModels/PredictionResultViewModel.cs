using System;
namespace RankPrediction_Web.Models
{
    public class PredictionResultViewModel
    {
        private int _id { get; set; }


        public PredictionResult PredictedResult { get; set; }

        /// <summary>
        /// 予測結果ランクの説明文を返します。
        /// </summary>
        public string RankDiscriptionstring
        {
            get
            {
                switch (PredictedResult.RankPictureBase64String)
                {
                    case "":
                        return "Apexプレデター相当です！";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 予測結果ランクのユーザーへの説明文を返します。
        /// </summary>
        public string RankResultString
        {
            get
            {
                switch (PredictedResult.RankPictureBase64String)
                {
                    case "":
                        return "Apexプレデター！素晴らしい腕をお持ちのようです。";
                    default:
                        return "";
                }
            }
        }

        public string testStr
        {
            get
            {
                return "test";
            }
        }

        /// <summary>
        /// 指定されたIDの結果を表示するViewModelを初期化します。
        /// </summary>
        /// <param name="id"></param>
        public PredictionResultViewModel(RankPredictionContext dbContext, int id)
        {
            _id = id;

            //対象IDの予測結果を取得する
            PredictedResult = new PredictionResult(dbContext, _id);

        }

    }
}
