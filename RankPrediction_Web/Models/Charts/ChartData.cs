using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RankPrediction_Web.Models.Charts
{
    /// <summary>
    /// チャートデータを表す基底クラス。
    /// </summary>
    public class ChartJsData : IChartData
    {
        public ChartJsData()
        {
            Config = new ChartConfig();
        }

        /// <summary>
        /// チャートを表示するコンフィグオブジェクト。
        /// チャートの種類・データを含んでいます。
        /// </summary>
        public ChartConfig Config { get; set; }

        string IChartData.GetChartConfigResponse()
        {

            return JsonSerializer.Serialize(Config);

        }
        string IChartData.GetChartDataResponse()
        {
            return JsonSerializer.Serialize(Config.Data);
        }

        string IChartData.GetChartDataSetsResponse()
        {
            return JsonSerializer.Serialize(Config.Data.DataSets);
        }
    }

}
