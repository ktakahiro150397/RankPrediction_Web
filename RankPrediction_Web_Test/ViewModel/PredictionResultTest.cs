using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankPrediction_Web.Models.DbContexts;
using System.Linq;
using RankPrediction_Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RankPrediction_Web_Test.ViewModel
{
    [TestClass]
    public class PredictionResultTest : DbContextBase
    {
       
        public PredictionResultTest() : base()
        {
        }

        //[TestMethod]
        //public void Constructor_Test_Exist_Id()
        //{
        //    //データID
        //    var actualId = 63;

        //    var expectedRank = db.Ranks.Where(rank => rank.RankId == 21).First();
        //    var actual = new PredictionResult(db, actualId);

        //    Assert.AreEqual(expectedRank.RankId, actual.PredictResult.RankId);
        //    Assert.AreEqual(expectedRank.RankName, actual.PredictResult.RankName);
        //}




    }
}
