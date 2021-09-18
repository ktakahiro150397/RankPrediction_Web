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
            IEnumerable<int?> target = new List<int?>() { 1, 2, 3, 4, 5 };
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
            IEnumerable<int?> target = new List<int?>() { 1, 2, 3, 4 };
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
            IEnumerable<int?> target = new List<int?>() { 3, 5, 1, 4, 2 };
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
            IEnumerable<int?> target = new List<int?>() { 3, 2, 4, 1 };
            double expected = (2D + 3D) / 2D;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        /// 偶数個のシーケンス_int型
        /// </summary>
        [TestMethod]
        public void Median_int_5()
        {
            IEnumerable<int?> target = new List<int?>() { 3 };
            double expected = 3;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        /// 偶数個のシーケンス_int型
        /// </summary>
        [TestMethod]
        public void Median_int_6()
        {
            IEnumerable<int?> target = new List<int?>() { 2,5 };
            double expected = (2D + 5D) / 2D;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        /// セレクタ関数を通じて中央値を取得
        /// </summary>
        [TestMethod]
        public void Median_int_7()
        {
            IEnumerable<int?> target = new List<int?>() { 3, 5, 1, 4, 2 };

            var valueClass = new Median_ValueClass<int?>(target);

            double expected = 3D;

            double? actual = valueClass.ValueList.Median(item => item.Value);

            Assert.AreEqual(expected, actual);



        }

        /// <summary>
        /// セレクタ関数を通じて中央値を取得
        /// </summary>
        [TestMethod]
        public void Median_int_8()
        {
            IEnumerable<int?> target = new List<int?>() { 3, 2, 4, 1 };
            double expected = (2D + 3D) / 2D;

            var valueClass = new Median_ValueClass<int?>(target);

            double? actual = valueClass.ValueList.Median(item => item.Value);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// セレクタ関数を通じて中央値を取得
        /// </summary>
        [TestMethod]
        public void Median_int_9()
        {
            IEnumerable<int?> target = new List<int?>() { 3 };
            double expected = 3D;

            var valueClass = new Median_ValueClass<int?>(target);

            double? actual = valueClass.ValueList.Median(item => item.Value);

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// セレクタ関数を通じて中央値を取得
        /// </summary>
        [TestMethod]
        public void Median_int_10()
        {
            IEnumerable<int?> target = new List<int?>() { 2, 5 };
            double expected = (2D + 5D) / 2D;

            var valueClass = new Median_ValueClass<int?>(target);

            double? actual = valueClass.ValueList.Median(item => item.Value);

            Assert.AreEqual(expected, actual);


        }


    }
}
