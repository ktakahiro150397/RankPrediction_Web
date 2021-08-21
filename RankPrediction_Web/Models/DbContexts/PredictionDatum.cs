using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RankPrediction_Web.Models
{
    public partial class PredictionDatum
    {
        public int Id { get; set; }


        public int SeasonId { get; set; }
        public int RankId { get; set; }

        public decimal KillDeathRatio { get; set; }

        public decimal AverageDamage { get; set; }

        public long MatchCounts { get; set; }

        public bool IsParty { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual SeasonName Season { get; set; }
    }
}
