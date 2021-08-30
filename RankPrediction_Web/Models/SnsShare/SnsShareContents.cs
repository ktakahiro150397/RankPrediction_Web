using System;
using System.Collections.Generic;

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
        /// リンク先のURL
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// タイトル
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
