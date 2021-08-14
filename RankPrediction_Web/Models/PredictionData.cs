using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models
{
    public partial class PredictionData
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int RankId { get; set; }
        public decimal KillDeathRatio { get; set; }
        public int AverageDamage { get; set; }
        public int MatchCounts { get; set; }
        public bool IsParty { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual SeasonName Season { get; set; }
    }
}
