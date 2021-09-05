using System;
namespace RankPrediction_Web.Models.SnsShare
{
    public class SnsShareModel
    {
        public SnsShareModel()
        {
            Twitter = new TwitterContents();
            FaceBook = new FaceBookContents();
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
                ShareTitle = title,
                ShareText = shareText
            };
            Twitter.HashTags.Add("APEX実力診断");
            Twitter.HashTags.Add("ApexLegends");

            FaceBook = new FaceBookContents()
            {
                ShareTitle = title,
                ShareText = shareText
            };
            OSNative = new SnsShareContents()
            {
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
