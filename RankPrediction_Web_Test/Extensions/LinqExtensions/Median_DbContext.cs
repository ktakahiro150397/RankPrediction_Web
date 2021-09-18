using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RankPrediction_Web.Extensions;

namespace RankPrediction_Web_Test.Extensions.LinqExtensions
{
    [TestClass]
    public class Median_DbContext : DbContextBase
    {
        [TestMethod]
        public void Median_DbContext_1()
        {

            var rankToAveKillRatio =
                 db
                 .PredictionData
                 .Join(
                     db.Ranks,
                     pred => pred.RankId,
                     rank => rank.RankId,
                     (pred, rank) => new
                     {
                         RankId = rank.RankId,
                         RankName = rank.RankNameJa,
                         KillRatio = pred.KillDeathRatio
                     }
                 )
                 .AsEnumerable();

            var test2 =
                 rankToAveKillRatio.GroupBy(item => new {item.RankId,item.RankName})
                 .Select(item => new
                 {
                     RankName = item.Key.RankName,
                     Value = item.Select(elem => elem.KillRatio).Median()
                 })
                 .ToList();
            Assert.AreEqual(123D, rankToAveKillRatio);

        }



    }
}
