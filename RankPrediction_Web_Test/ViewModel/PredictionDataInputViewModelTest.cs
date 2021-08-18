using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankPrediction_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankPrediction_Web_Test.ViewModel
{

    [TestClass]
    public class PredictionDataInputViewModelTest
    {

        [TestMethod]
        public void MatchCount_long()
        {
            long expect = 12345;
            string input = "12345";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_k_NoDecimal()
        {
            long expect = 12345000;
            string input = "12345k";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_K_NoDecimal()
        {
            long expect = 12345000;
            string input = "12345K";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_K_Decimal_1()
        {
            long expect = 12345600;
            string input = "12345.6K";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_K_Decimal_2()
        {
            long expect = 12345670;
            string input = "12345.67K";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_K_Decimal_3()
        {
            long expect = 12345678;
            string input = "12345.678K";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }


        [TestMethod]
        public void MatchCount_Suffix_m_NoDecimal()
        {
            long expect = 12345000000;
            string input = "12345m";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_M_NoDecimal()
        {
            long expect = 12345000000;
            string input = "12345M";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_M_Decimal_1()
        {
            long expect = 12345600000;
            string input = "12345.6M";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_M_Decimal_2()
        {
            long expect = 12345670000;
            string input = "12345.67M";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Suffix_M_Decimal_3()
        {
            long expect = 12345678000;
            string input = "12345.678M";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_ZeroPrefix()
        {
            long expect = 1234;
            string input = "01234";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_ZeroPrefix_Suffix_K()
        {
            long expect = 1234500;
            string input = "01234.5K";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }


        [TestMethod]
        public void MatchCount_ZeroPrefix_Suffix_M()
        {
            long expect = 1234500000;
            string input = "01234.5M";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }



        [TestMethod]
        public void MatchCount_Incorrect_1()
        {
            long expect = -1;
            string input = "abcd";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Incorrect_2()
        {
            long expect = -1;
            string input = "1d";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

        [TestMethod]
        public void MatchCount_Incorrect_3()
        {
            long expect = -1;
            string input = "0b9";

            var vm = new PredictionDataInputViewModel();
            vm.MatchCounts = input;

            Assert.AreEqual(expect, vm.MatchCount_long);

        }

    }
}
