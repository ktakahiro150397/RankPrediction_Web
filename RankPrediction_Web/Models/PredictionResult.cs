﻿using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        /// 指定のDBコンテキストを使用し、対象IDの予測結果を初期化します。
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="id"></param>
        public PredictionResult(RankPredictionContext dbContext, int id)
        {
            _id = id;

            // TODO : kokoni kekka wo ireru
            PredictResult = dbContext.Ranks.Where(item => item.RankId == 21).First();

        }
    }
}