using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class RanksGeneral
    {
        public RanksGeneral()
        {
            RankAmazonUrls = new HashSet<RankAmazonUrl>();
            RankRelations = new HashSet<RankRelation>();
        }

        public int Id { get; set; }
        public string RankName { get; set; }
        public string RankNameJa { get; set; }

        public virtual ICollection<RankAmazonUrl> RankAmazonUrls { get; set; }
        public virtual ICollection<RankRelation> RankRelations { get; set; }
    }
}
