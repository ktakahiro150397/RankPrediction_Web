using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankPrediction_Web_Test.Extensions.LinqExtensions
{

    /// <summary>
    /// セレクタ関数テスト用のバリュークラスを保持するクラス。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Median_ValueClass<T>
    {
        public IList<Median_Value<T>> ValueList;

        public Median_ValueClass(IEnumerable<T> src)
        {
            ValueList = new List<Median_Value<T>>();

            int i = 0;
            foreach(var item in src)
            {
                ValueList.Add(
                    new Median_Value<T>
                    {
                        seq = i,
                        Value = item
                    }
                ); ;
                i++;
            }
        }

    }

    /// <summary>
    /// セレクタ関数テスト用のバリュークラス。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Median_Value<T>
    {
        public T Value;
        public int seq;
    }
}
