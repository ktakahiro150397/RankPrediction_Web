using System;
using RankPrediction_Web.Models.DbContexts;

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
                switch (PredictedResult.PredictResult.RankId)
                {
                    case 0:
                        return "ブロンズ4相当です！";
                    case 1:
                        return "ブロンズ3相当です！";
                    case 2:
                        return "ブロンズ2相当です！";
                    case 3:
                        return "ブロンズ1相当です！";
                    case 4:
                        return "シルバー4相当です！";
                    case 5:
                        return "シルバー3相当です！";
                    case 6:
                        return "シルバー2相当です！";
                    case 7:
                        return "シルバー1相当です！";
                    case 8:
                        return "ゴールド4相当です！";
                    case 9:
                        return "ゴールド3相当です！";
                    case 10:
                        return "ゴールド2相当です！";
                    case 11:
                        return "ゴールド1相当です！";
                    case 12:
                        return "プラチナ4相当です！";
                    case 13:
                        return "プラチナ3相当です！";
                    case 14:
                        return "プラチナ2相当です！";
                    case 15:
                        return "プラチナ1相当です！";
                    case 16:
                        return "ダイヤモンド4相当です！！";
                    case 17:
                        return "ダイヤモンド3相当です！！";
                    case 18:
                        return "ダイヤモンド2相当です！！";
                    case 19:
                        return "ダイヤモンド1相当です！！";
                    case 20:
                        return "マスターランク相当です！！！";
                    case 21:
                        return "Apex Predator 相当です！！！";
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
                switch (PredictedResult.PredictResult.RankId)
                {
                    case 0:
                        return "ブロンズ4 相当の説明文を入れる";
                    case 1:
                        return "ブロンズ3 相当の説明文を入れる";
                    case 2:
                        return "ブロンズ2 相当の説明文を入れる";
                    case 3:
                        return "ブロンズ1 相当の説明文を入れる";
                    case 4:
                        return "シルバー4 相当の説明文を入れる";
                    case 5:
                        return "シルバー3 相当の説明文を入れる";
                    case 6:
                        return "シルバー2 相当の説明文を入れる";
                    case 7:
                        return "シルバー1 相当の説明文を入れる";
                    case 8:
                        return "ゴールド4 相当の説明文を入れる";
                    case 9:
                        return "ゴールド3 相当の説明文を入れる";
                    case 10:
                        return "ゴールド2 相当の説明文を入れる";
                    case 11:
                        return "ゴールド1 相当の説明文を入れる";
                    case 12:
                        return "プラチナ4 相当の説明文を入れる";
                    case 13:
                        return "プラチナ3 相当の説明文を入れる";
                    case 14:
                        return "プラチナ2 相当の説明文を入れる";
                    case 15:
                        return "プラチナ1 相当の説明文を入れる";
                    case 16:
                        return "ダイヤモンド4 相当の説明文を入れる！";
                    case 17:
                        return "ダイヤモンド3 相当の説明文を入れる！";
                    case 18:
                        return "ダイヤモンド2 相当の説明文を入れる！";
                    case 19:
                        return "ダイヤモンド1 相当の説明文を入れる！";
                    case 20:
                        return "マスターランク 相当の説明文を入れる！！";
                    case 21:
                        return "Apex Predator  相当の説明文を入れる！！";
                    default:
                        return "";
                }
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
