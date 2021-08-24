using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class Rank
    {
        public Rank()
        {
            PredictionData = new HashSet<PredictionDatum>();
        }

        public int RankId { get; set; }
        public string RankName { get; set; }

        public virtual ICollection<PredictionDatum> PredictionData { get; set; }
    }
}
