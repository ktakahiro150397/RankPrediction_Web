using System;
using System.Security.Claims;

namespace RankPrediction_Web.Models.Authentication
{

    /// <summary>
    /// Steam認証情報を表します。
    /// </summary>
    public class SteamUserInfo
    {

        /// <summary>
        /// Steamでの認証情報が格納されている<see cref="System.Security.Claims.Claim"/>型のデータから、
        /// 認証情報を取得します。
        /// </summary>
        /// <param name="user"></param>
        public SteamUserInfo(Claim user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Steam名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Steamユーザーに固有のID。
        /// </summary>
        public string UniqueId { get; set; }
    }
}
