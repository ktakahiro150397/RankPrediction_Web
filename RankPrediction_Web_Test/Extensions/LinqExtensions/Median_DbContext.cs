using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RankPrediction_Web.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RankPrediction_Web_Test.Extensions.LinqExtensions
{

    [TestClass]
    public class Median_DbContext : DbContextBase
    {
        public Median_DbContext()
        {
        }

        [TestMethod]
        public void Median_DbContext_1()
        {


            var med = db.PredictionData.Median(item => item.MatchCounts);

            var rankToAveKillRatio =
                db
                .PredictionData
                .GroupBy(item => new { item.RankId, item.Rank.RankNameJa })
                .ToList();

            Assert.AreEqual(123D, med);
        }

    }
}
