using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class RankRelation
    {
        public int Id { get; set; }
        public int RankId { get; set; }
        public int RankGeneralId { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual RanksGeneral RankGeneral { get; set; }
    }
}
