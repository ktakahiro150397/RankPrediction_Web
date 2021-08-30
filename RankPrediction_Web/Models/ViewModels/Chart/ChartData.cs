using System;
using System.Collections.Generic;

namespace RankPrediction_Web.Models.ViewModels.Chart
{
    public class ChartData
    {
        public ChartData()
        {
            data = new List<double>();
        }

        public IList<double> data { get; set; }

        public string DataValues
        {
            get
            {
                return String.Join(",", data);
            }
        }
    }
}
