using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models
{
    public partial class Rank
    {
        public Rank()
        {
            PlayerData = new HashSet<PlayerDatum>();
        }

        public int RankId { get; set; }
        public string RankName { get; set; }

        public virtual ICollection<PlayerDatum> PlayerData { get; set; }
    }
}
