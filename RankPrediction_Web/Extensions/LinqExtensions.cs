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



            throw new NotImplementedException();
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


            throw new NotImplementedException();
        }

    }
}
