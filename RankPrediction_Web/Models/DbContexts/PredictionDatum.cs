using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class PredictionDatum
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int RankId { get; set; }
        public double? KillDeathRatio { get; set; }
        public double? AverageDamage { get; set; }
        public long? MatchCounts { get; set; }
        public bool IsParty { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual SeasonName Season { get; set; }
    }
}
