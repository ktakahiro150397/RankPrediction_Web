using System;
using System.Collections.Generic;
using System.Linq;

namespace RankPrediction_Web.Models.ViewModels.Chart
{

    /// <summary>
    /// 画面に表示するチャートの実際の値を保持します。
    /// </summary>
    public class ChartData
    {
        public ChartData()
        {
            ChartValues = new List<ChartValues>();
        }

        /// <summary>
        /// チャートに表示するデータのリスト。
        /// </summary>
        public IList<ChartValues> ChartValues { get; set; }

        /// <summary>
        /// チャートのタイトル。
        /// </summary>
        public string ChartLabel { get; set; }

        /// <summary>
        /// データラベルをカンマ区切りした文字列を返します。
        /// </summary>
        public string LabelsString
        {
            get
            {
                return String.Join(",", ChartValues.Select(item => item.LabelWithQuote));
            }
        }

        /// <summary>
        /// データ値をカンマ区切りした文字列を返します。
        /// </summary>
        public string DataString
        {
            get
            {
                return String.Join(",", ChartValues.Select(item => item.Value));
            }
        }

        /// <summary>
        /// チャートタイトルをシングルクォートで囲った文字列を返します。
        /// </summary>
        public string ChartLabelString
        {
            get
            {
                return "'" + ChartLabel + "'";
            }
        }

    }


    /// <summary>
    /// チャートに表示する単一のラベル・値を保持します。
    /// </summary>
    public class ChartValues
    {
        public ChartValues()
        {
        }

        /// <summary>
        /// データラベル。
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// データの値。
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// シングルクォート付きラベルを返します。
        /// /// </summary>
        public string LabelWithQuote
        {
            get
            {
                return "'" + Label + "'";
            }
        }

    }
}
