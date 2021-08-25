using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RankPrediction_Web.Models.DbContexts;

namespace RankPrediction_Web.Models
{
    public class PredictionResult
    {

        private int _id { get; set; }

        /// <summary>
        /// 指定IDデータを元に、予測したランク結果を返します。
        /// </summary>
        public Rank PredictResult { get; set; }

        /// <summary>
        /// ランク画像を表すバイト配列。
        /// </summary>
        private byte[] RankPicture
        {
            get
            {
                // TODO : Rank Class kara gazou bukko nuku
                return null;
            }
        }

        /// <summary>
        /// 設定されているランク画像のBase64エンコード文字列を返します。設定されていない場合、空文字を返します。
        /// </summary>
        public string RankPictureBase64String
        {
            get
            {
                if (RankPicture == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToBase64String(RankPicture);
                }
            }
        }

        /// <summary>
        /// 指定のDBコンテキストを使用し、対象データIDの予測結果を初期化します。
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="id"></param>
        public PredictionResult(RankPredictionContext dbContext, int id)
        {
            _id = id;

            if (dbContext.PyRankPredictions.Any(item => item.SourceDataId == _id))
            {
                // 結果が存在する場合、その結果を取得する
                // 
                PredictResult = dbContext.PyRankPredictions
                    .Where(item => item.SourceDataId == _id)
                    .Join(
                        dbContext.Ranks,
                        pyRank => pyRank.PredictResultRankId,
                        rank => rank.RankId,
                        (_, rank) => new Rank()
                        {
                            RankId = rank.RankId,
                            RankName = rank.RankName,
                            RankNameJa = rank.RankNameJa

                        })
                    .First();
            }
            else
            {
                //結果が存在しない場合、SPから取得する

                if(dbContext.PredictionData.Any(item=> item.Id == _id))
                {
                //対象のデータIDが存在する場合はSP実行
                var execRawSql = "EXEC ml_predict.get_predict_result_by_id {0}";
                var predictRes = dbContext.Ranks.FromSqlRaw(execRawSql, id).ToList();

                PredictResult = predictRes.First();

                }
                else
                {
                    //存在しないデータID
                    PredictResult = null;
                    
                }
            }

        }
    }
}
