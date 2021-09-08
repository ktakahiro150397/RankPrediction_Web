using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RankPrediction_Web.Models.Charts
{

    /// <summary>
    /// チャートを表示するコンフィグオブジェクト。
    /// </summary>
    public class ChartConfig
    {

        public ChartConfig()
        {
            ChartTypeValue = ChartType.bar;
            Data = new ChartConfigData();
        }

        /// <summary>
        /// チャートの種類。
        /// </summary>
        [JsonIgnore]
        public ChartType ChartTypeValue { get; set; }

        [JsonPropertyName("type")]
        public string ChartTypeString
        {
            get
            {
                return ChartTypeValue.ToString();
            }
        }

        /// <summary>
        /// チャートのデータ。
        /// </summary>
        [JsonPropertyName("data")]
        public ChartConfigData Data { get; set; }

        /// <summary>
        /// チャートのオプションデータ。
        /// </summary>
        [JsonPropertyName("options")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ChartConfigOption Options { get; set; }

    }

    /// <summary>
    /// チャートデータを表します。
    /// </summary>
    public class ChartConfigData
    {
        public ChartConfigData()
        {
            Labels = new List<string>();
            DataSets = new List<DataSetItem>();
        }

        /// <summary>
        /// チャートに表示するラベル。
        /// </summary>
        [JsonPropertyName("labels")]
        public IList<string> Labels { get; set; }

        /// <summary>
        /// チャートに表示するデータセットのリスト。
        /// </summary>
        [JsonPropertyName("datasets")]
        public IList<DataSetItem> DataSets { get; set; }

    }

    /// <summary>
    /// チャートのデータセットを表します。
    /// </summary>
    public class DataSetItem
    {
        public DataSetItem()
        {
            //Data = new List<int>();
            BackGroundColor = new List<string>()
            {
                "rgba(255, 99, 132, 0.2)",
                "rgba(54, 162, 235, 0.2)",
                "rgba(255, 206, 86, 0.2)",
                "rgba(75, 192, 192, 0.2)",
                "rgba(153, 102, 255, 0.2)",
                "rgba(255, 159, 64, 0.2)",
                "rgba(255, 159, 64, 0.2)",
            };
            BorderColor = new List<string>()
            {
                "rgba(255, 99, 132, 1)",
                "rgba(54, 162, 235, 1)",
                "rgba(255, 206, 86, 1)",
                "rgba(75, 192, 192, 1)",
                "rgba(153, 102, 255, 1)",
                "rgba(255, 159, 64, 1)",
                "rgba(255, 159, 64, 1)",
            };
        }

        /// <summary>
        /// チャートのラベルを表します。
        /// </summary>
        [JsonPropertyName("label")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Label { get; set; }

        /// <summary>
        /// チャートに表示する値を表します。
        /// </summary>
        [JsonPropertyName("data")]
        public IList<int> Data { get; set; }

        /// <summary>
        /// チャートグラフの背景色を文字列で指定します。
        /// </summary>
        [JsonPropertyName("backgroundColor")]
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingDefault)]
        public IList<string> BackGroundColor { get; set; }

        /// <summary>
        /// チャートグラフの枠色を文字列で指定します。
        /// </summary>
        [JsonPropertyName("borderColor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<string> BorderColor { get; set; }

    }

    public class ChartConfigOption
    {
        public ChartConfigOption()
        {
        }

        public ChartConfigOption(IList<int> data)
        {
            Scales = new
            {
                y = new
                {
                    min = 0,
                    max = data.Max() + 10
                }
            };
        }

        [JsonPropertyName("scales")]
        public object Scales { get; set; }

    }

}
