using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class SeasonName
    {
        public SeasonName()
        {
            PredictionData = new HashSet<PredictionDatum>();
        }

        public int SeasonId { get; set; }
        public string SeasonName1 { get; set; }
        public string SeasonNameJa { get; set; }
        public int DisplaySeq { get; set; }

        public virtual ICollection<PredictionDatum> PredictionData { get; set; }
    }
}
