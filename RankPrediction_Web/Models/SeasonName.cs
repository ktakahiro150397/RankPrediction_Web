using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models
{
    public partial class SeasonName
    {
        public SeasonName()
        {
            PlayerData = new HashSet<PlayerDatum>();
        }

        public int SeasonId { get; set; }
        public string SeasonName1 { get; set; }

        public virtual ICollection<PlayerDatum> PlayerData { get; set; }
    }
}
