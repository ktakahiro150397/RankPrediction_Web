﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RankPrediction_Web.Extensions;

namespace RankPrediction_Web_Test.Extensions.LinqExtensions
{

    [TestClass]
    public class Median
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

        /// <summary>
        /// 奇数個のシーケンス_double型_.0
        /// </summary>
        [TestMethod]
        public void Median_double_1()
        {
            IEnumerable<double> target = new List<double>() { 1.0D, 2.0D, 3.0D, 4.0D, 5.0D };
            double expected = 3D;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// 偶数個のシーケンス_double型_.0
        /// </summary>
        [TestMethod]
        public void Median_double_2()
        {
            IEnumerable<double> target = new List<double>() { 1.0D, 2.0D, 3.0D, 4.0D };
            double expected = (2 + 3) / 2;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// 奇数個のシーケンス_double型
        /// </summary>
        [TestMethod]
        public void Median_double_3()
        {
            IEnumerable<double> target = new List<double>() { 1.3, 2, 7, 4.5, 2.9 };
            double expected = 2.9;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// 偶数個のシーケンス_double型_.0
        /// </summary>
        [TestMethod]
        public void Median_double_4()
        {
            IEnumerable<double> target = new List<double>() { 1.3, 2, 4.5, 2.9 };
            double expected = (2 + 2.9) / 2;

            double actual = target.Median();

            Assert.AreEqual(expected, actual);

        }

    }
}
