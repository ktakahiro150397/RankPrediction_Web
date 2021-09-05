using System;
using System.Collections.Generic;
using System.Web;

namespace RankPrediction_Web.Models.SnsShare
{
    /// <summary>
    /// SNS共有に使用する基本データを表します。
    /// </summary>s
    public class SnsShareContents
    {
        public SnsShareContents()
        {
        }

        /// <summary>
        /// ボタンに割り当てるリンク先のURL。
        /// </summary>
        public string LinkUrl {
            get
            {

                //var query = new HttpUtility.ParseQueryString("");


                //var urlBuilder = new UriBuilder()
                //{
                //    Query =
                //}

                return "https://twitter.com/share?url=https://apexrankprediction.azurewebsites.net/&text=AIでAPEXの実力を診断してみよう&hashtags=APEX実力診断";
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

        /// <summary>
        /// Twitter共有のベースとなるURL。
        /// </summary>
        private const string BaseUrl = "https://twitter.com/share";


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

        public IList<string> HashTags { get; set; }

        public string HashTagsCommaSeparete
        {
            get
            {
                return String.Join(",", HashTags);
            }
        }
    }

}
