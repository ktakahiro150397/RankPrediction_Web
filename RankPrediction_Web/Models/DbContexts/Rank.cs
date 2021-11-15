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
            RankRelations = new HashSet<RankRelation>();
        }

        public int RankId { get; set; }
        public string RankName { get; set; }
        public string RankNameJa { get; set; }
        public int DisplaySeq { get; set; }
        public byte[] RankPic { get; set; }
        public string RankDesc { get; set; }
        public string RankDescJa { get; set; }

        public virtual ICollection<PredictionDatum> PredictionData { get; set; }
        public virtual ICollection<RankRelation> RankRelations { get; set; }
    }
}
