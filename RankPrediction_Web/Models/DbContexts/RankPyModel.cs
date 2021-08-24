using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class RankPyModel
    {
        public string ModelName { get; set; }
        public byte[] Model { get; set; }
    }
}
