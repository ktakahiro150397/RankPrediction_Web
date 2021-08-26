﻿using System;
namespace RankPrediction_Web.Models.ViewModels
{
    public class CalcViewModel
    {
        public CalcViewModel()
        {
        }

        /// <summary>
        /// 結果表示を要求する入力データID
        /// </summary>
        public int? DataId {get;set;}

        /// <summary>
        /// 画面に表示する名言
        /// </summary>
        public string Saying { get; set; }

    }
}
