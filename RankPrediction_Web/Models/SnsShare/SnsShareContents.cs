using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.AspNetCore.WebUtilities;

namespace RankPrediction_Web.Models.SnsShare
{
    /// <summary>
    /// SNS共有に使用する基本データを表します。
    /// </summary>s
    public class SnsShareContents
    {
        public SnsShareContents()
        {
            //シェアするベースURL。
            ShareUrl = "https://apexrankprediction.azurewebsites.net";
        }

        /// <summary>
        /// ボタンに割り当てるリンク先のURL。
        /// </summary>
        public virtual string LinkUrl
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 共有するこのサイトへのURL。
        /// </summary>
        public string ShareUrl { get; set; }

        /// <summary>
        /// 共有タイトル
        /// </summary>
        public string ShareTitle { get; set; }

        /// <summary>
        /// 共有本文
        /// </summary>
        public string ShareText { get; set; }


    }

    /// <summary>
    /// Twitter共有に使用するデータを表します。
    /// </summary>
    public class TwitterContents : SnsShareContents
    {
        public TwitterContents() : base()
        {
            HashTags = new List<string>();
        }

        /// <summary>
        /// Twitter共有のベースとなるURL。
        /// </summary>
        private const string BaseUrl = "https://twitter.com/share";

        public IList<string> HashTags { get; set; }

        public string HashTagsCommaSeparete
        {
            get
            {
                return String.Join(",", HashTags);
            }
        }

        public override string LinkUrl
        {
            get
            {
                //クエリ文字列のディクショナリ
                var queryDic = new Dictionary<string, string>()
                {
                    {"url",ShareUrl },
                    {"text",  ShareTitle + "\n" + ShareText},
                    {"hashtags", HashTagsCommaSeparete }
                };


                //クエリパラメータの追加
                var uri = QueryHelpers.AddQueryString(BaseUrl, queryDic);

                return new Uri(uri).ToString();
            }
        }

    }

}
