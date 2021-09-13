using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankPrediction_Web.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// 対象のシーケンスから、中央値を計算します。
        /// </summary>
        /// <returns></returns>
        public static double Median(this IEnumerable<int> src)
        {

            if(src == null)
            {
                throw new InvalidOperationException("Can't calculate median from null sequence");
            }

            if (src.Count() == 0)
            {
                throw new InvalidOperationException("Can't calculate median from blank sequence");
            }

            //対象のシーケンスを昇順にソート
            var sortedSeq = src.OrderBy(item => item).ToArray();

            //シーケンスの数に応じて処理を分岐
            if(sortedSeq.Count() % 2 == 0)
            {
                //偶数個
                var retSeq = sortedSeq.Count() / 2;
                return ((double)sortedSeq[retSeq - 1] + (double)sortedSeq[retSeq]) / 2;
            }
            else
            {
                //奇数個
                var retSeq = sortedSeq.Count() / 2;
                return sortedSeq[retSeq];
            }

        }

        
        /// <summary>
        /// 対象のシーケンスから、中央値を計算します。
        /// </summary>
        /// <returns></returns>
        public static double Median(this IEnumerable<double> src)
        {
            if (src == null)
            {
                throw new InvalidOperationException("Can't calculate median from null sequence");
            }

            if (src.Count() == 0)
            {
                throw new InvalidOperationException("Can't calculate median from blank sequence");
            }


            //対象のシーケンスを昇順にソート
            var sortedSeq = src.OrderBy(item => item).ToArray();

            //シーケンスの数に応じて処理を分岐
            if (sortedSeq.Count() % 2 == 0)
            {
                //偶数個
                var retSeq = sortedSeq.Count() / 2;
                return (sortedSeq[retSeq - 1] + sortedSeq[retSeq]) / 2;
            }
            else
            {
                //奇数個
                var retSeq = sortedSeq.Count() / 2;
                return sortedSeq[retSeq];
            }

        }

        /// <summary>
        /// 対象のシーケンスから、セレクタ関数を通じて取得した値の中央値を取得します。
        /// </summary>
        /// <param name="src"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static double Median<T>(this IEnumerable<T> src, Func<T, int> selector)
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// 対象のシーケンスから、セレクタ関数を通じて取得した値の中央値を取得します。
        /// </summary>
        /// <param name="src"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static double Median<T>(this IEnumerable<T> src, Func<T, double> selector)
        {

            throw new NotImplementedException();
        }

    }
}
