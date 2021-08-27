using System;
using System.Collections.Generic;

#nullable disable

namespace RankPrediction_Web.Models.DbContexts
{
    public partial class Saying
    {
        public int Id { get; set; }
        public string Saying1 { get; set; }
        public string SayingJa { get; set; }
        public string SayingBy { get; set; }
        public string SayingByJa { get; set; }
    }
}
