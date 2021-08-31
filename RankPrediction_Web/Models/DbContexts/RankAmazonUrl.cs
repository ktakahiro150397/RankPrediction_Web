using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class RankAmazonUrl
    {
        public int Id { get; set; }
        public int RankGeneralId { get; set; }
        public string AmazonUrl { get; set; }

        public virtual RanksGeneral RankGeneral { get; set; }
    }
}
