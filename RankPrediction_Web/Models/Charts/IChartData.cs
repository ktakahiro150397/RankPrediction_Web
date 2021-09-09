using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankPrediction_Web.Models.Charts
{
    /// <summary>
    /// チャートデータを表すクラスが実装するインターフェース。
    /// </summary>
    interface IChartData
    {
        /// <summary>
        /// 設定されているチャートのコンフィグ情報全体を、Json形式の文字列として返します。
        /// </summary>
        /// <returns></returns>
        public string GetChartConfigResponse();

        /// <summary>
        /// 設定されているデータの情報を、Json形式の文字列として返します。
        /// </summary>
        /// <returns></returns>
        public string GetChartDataResponse();

        /// <summary>
        /// 設定されているデータセットの情報を、Json形式の文字列として返します。
        /// </summary>
        /// <returns></returns>
        public string GetChartDataSetsResponse();

    }
}
