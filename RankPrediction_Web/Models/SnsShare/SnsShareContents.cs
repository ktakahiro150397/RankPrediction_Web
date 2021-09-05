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
        /// 共有ボタンのベースとなるURL。
        /// </summary>
        protected string BaseUrl { get; set; }

        /// <summary>
        /// ボタンに割り当てるリンク先のURL。
        /// </summary>
        public virtual string LinkUrl { get; }

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
            BaseUrl = "https://twitter.com/share";
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

    /// <summary>
    /// Facebook共有に使用するデータを表します。
    /// </summary>
    public class FaceBookContents : SnsShareContents
    {

        public FaceBookContents() : base()
        {
            BaseUrl = "https://www.facebook.com/sharer/sharer.php";
        }

        public override string LinkUrl
        {
            get
            {
                var queryDic = new Dictionary<string, string>()
                {
                    {"u",ShareUrl }
                };

                var uri = QueryHelpers.AddQueryString(BaseUrl, queryDic);

                return new Uri(uri).ToString();
            }
        }


    }

    /// <summary>
    /// Pocket共有に使用するデータを表します。
    /// </summary>
    public class PocketContents : SnsShareContents
    {
        public PocketContents() : base()
        {
            BaseUrl = "https://getpocket.com/edit";
        }

        public override string LinkUrl
        {
            get
            {
                var queryDic = new Dictionary<string, string>()
                {
                    {"url",ShareUrl }
                };

                var uri = QueryHelpers.AddQueryString(BaseUrl, queryDic);

                return new Uri(uri).ToString();
            }
        }
    }
}

