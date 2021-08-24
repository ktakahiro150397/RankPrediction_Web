using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class PyRankPrediction
    {
        public int Id { get; set; }
        public int SourceDataId { get; set; }
        public int? PredictResultRankId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
