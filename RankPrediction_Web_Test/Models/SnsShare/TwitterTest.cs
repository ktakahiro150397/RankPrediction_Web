using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankPrediction_Web.Models.SnsShare;

namespace RankPrediction_Web_Test.Models.SnsShare
{

    [TestClass]
    public class TwitterTest
    {
        public TwitterTest()
        { }

        [TestMethod]
        public void TwitterModelTest_1()
        {

            var actual = new TwitterContents()
            {
                ShareTitle = "AIでAPEXの実力を診断してみよう！",
                ShareText = "秘められた実力が分かるかも！",
                ShareUrl = "https://apexrankprediction.azurewebsites.net",
                HashTags = new List<string>()
                {
                    "APEX実力診断","ApexLegends"
                }
            };


            var expectedUrl = "https://twitter.com/share" +
                "?url=https%3A%2F%2Fapexrankprediction.azurewebsites.net" +
                "&text=AIでAPEXの実力を診断してみよう！%0A秘められた実力が分かるかも！" +
                "&hashtags=APEX実力診断,ApexLegends";

            Assert.AreEqual(expectedUrl, actual.LinkUrl);

        }

        [TestMethod]
        public void TwitterModelTest_2()
        {

            var actual = new TwitterContents()
            {
                ShareTitle = "AIでAPEXの実力を診断してみました！",
                ShareText = "私の診断結果は「Apexプレデター」でした！",
                ShareUrl = "https://apexrankprediction.azurewebsites.net",
                HashTags = new List<string>()
                {
                    "APEX実力診断","ApexLegends","Apexプレデター"
                }
            };


            var expectedUrl = "https://twitter.com/share" +
                "?url=https%3A%2F%2Fapexrankprediction.azurewebsites.net" +
                "&text=AIでAPEXの実力を診断してみました！%0A私の診断結果は「Apexプレデター」でした！" +
                "&hashtags=APEX実力診断,ApexLegends,Apexプレデター";

            Assert.AreEqual(expectedUrl, actual.LinkUrl);

        }

    }
}
