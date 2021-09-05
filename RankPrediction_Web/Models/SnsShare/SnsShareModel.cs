using System;
namespace RankPrediction_Web.Models.SnsShare
{
    public class SnsShareModel
    {
        public SnsShareModel()
        {
            Twitter = new TwitterContents();
            FaceBook = new SnsShareContents();
            OSNative = new SnsShareContents();
        }

        /// <summary>
        /// 指定のタイトル・テキストでSNS共有データを初期化します。
        /// </summary>
        /// <param name="shareText"></param>
        public SnsShareModel(string title,string shareText)
        {

            Twitter = new TwitterContents()
            {
                //LinkUrl = "https://twitter.com/share?url=https://apexrankprediction.azurewebsites.net/&text=AIでAPEXの実力を診断してみよう&hashtags=APEX実力診断",
                ShareTitle = title,
                ShareText = shareText
            };
            FaceBook = new SnsShareContents()
            {
                //LinkUrl = "www.google.com",
                ShareTitle = title,
                ShareText = shareText
            };
            OSNative = new SnsShareContents()
            {
                //LinkUrl = "www.google.com",
                ShareTitle = title,
                ShareText = shareText
            };

        }

        /// <summary>
        /// Twitterの共有データ。
        /// </summary>
        public TwitterContents Twitter { get; set; }

        /// <summary>
        /// Facebookの共有データ。
        /// </summary>
        public SnsShareContents FaceBook { get; set; }

        /// <summary>
        /// OS汎用共有の共有データ。
        /// </summary>
        public SnsShareContents OSNative { get; set; }

    }
}
