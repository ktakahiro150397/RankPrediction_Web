using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankPrediction_Web.Extensions;

namespace RankPrediction_Web_Test.Extensions.LinqExtensions
{
    [TestClass]
    public class Median_Error
    {

        /// <summary>
        /// int型空白シーケンス
        /// </summary>
        [TestMethod]
        public void Median_int_Blank_Sequence()
        {
            IEnumerable<int> target = new List<int>();

            var ex = Assert.ThrowsException<InvalidOperationException>(() => target.Median());

            Assert.AreEqual(ex.Message, "Can't calculate median from blank sequence");

        }

        /// <summary>
        /// int型nullシーケンス
        /// </summary>
        [TestMethod]
        public void Median_int_null_Sequence()
        {
            IEnumerable<int> target = null;

            var ex = Assert.ThrowsException<InvalidOperationException>(() => target.Median());

            Assert.AreEqual(ex.Message, "Can't calculate median from null sequence");

        }

        /// <summary>
        /// double型空白シーケンス
        /// </summary>
        [TestMethod]
        public void Median_double_Blank_Sequence()
        {
            IEnumerable<double> target = new List<double>();

            var ex = Assert.ThrowsException<InvalidOperationException>(() => target.Median());

            Assert.AreEqual(ex.Message, "Can't calculate median from blank sequence");

        }

        /// <summary>
        /// double型nullシーケンス
        /// </summary>
        [TestMethod]
        public void Median_double_null_Sequence()
        {
            IEnumerable<double> target = null;

            var ex = Assert.ThrowsException<InvalidOperationException>(() => target.Median());

            Assert.AreEqual(ex.Message, "Can't calculate median from null sequence");

        }


    }
}
