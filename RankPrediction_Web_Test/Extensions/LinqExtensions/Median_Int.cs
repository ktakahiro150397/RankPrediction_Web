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
    public class Median_Int
    {

        /// <summary>
        /// 奇数個のシーケンス_int型
        /// </summary>
        [TestMethod]
        public void Median_int_1()
        {
            IEnumerable<int> target = new List<int>() { 1, 2, 3, 4, 5 };
            double expected = 3D;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// 偶数個のシーケンス_int型
        /// </summary>
        [TestMethod]
        public void Median_int_2()
        {
            IEnumerable<int> target = new List<int>() { 1, 2, 3, 4 };
            double expected = (2D + 3D) / 2D;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// 奇数個のシーケンス_int型
        /// </summary>
        [TestMethod]
        public void Median_int_3()
        {
            IEnumerable<int> target = new List<int>() { 3, 5, 1, 4, 2 };
            double expected = 3D;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// 偶数個のシーケンス_int型
        /// </summary>
        [TestMethod]
        public void Median_int_4()
        {
            IEnumerable<int> target = new List<int>() { 3, 2, 4, 1 };
            double expected = (2D + 3D) / 2D;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);


        }
    }
}
