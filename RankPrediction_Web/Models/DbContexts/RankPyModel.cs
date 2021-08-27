using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class RankPyModel
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public bool IsMatchcountsContain { get; set; }
        public byte[] ModelData { get; set; }

        public virtual SeasonName Season { get; set; }
    }
}
