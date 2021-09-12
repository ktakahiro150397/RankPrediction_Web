using System;
using System.Security.Claims;
using RankPrediction_Web.Models.DbContexts;

namespace RankPrediction_Web.Models.Authentication
{

    /// <summary>
    /// このサイトのユーザー情報を表します。
    /// </summary>
    public class AuthenticatedUser
    {

        [Obsolete("テスト用")]
        public AuthenticatedUser()
        {
            _displayName = "test hoge fu ga";
            _biography = "test - code me !";
            _isAuthenticate = false;
        }

        /// <summary>
        /// 既に認証済みのデータを使用して初期化します。
        /// </summary>
        /// <param name="User"></param>
        public AuthenticatedUser(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 指定のIDでユーザー情報を取得し、初期化します。
        /// </summary>
        /// <param name="dbContext"></param>
        public AuthenticatedUser(RankPredictionContext dbContext, int id)
        {
            throw new NotImplementedException();
        }

        private bool _isAuthenticate;
        private bool _isPublic;
        private string _displayName;
        private string _biography;
        private SteamUserInfo _steamUserInfo;

        /// <summary>
        /// このユーザーが認証されている場合True。
        /// </summary>
        public bool IsAuthenticate
        {
            get
            {
                return _isAuthenticate;
            }
        }

        /// <summary>
        /// このユーザーのSteam認証情報を保持します。
        /// </summary>
        public SteamUserInfo SteamUserInfo
        {
            get
            {
                return _steamUserInfo;
            }
        }

        /// <summary>
        /// このユーザーの表示名。
        /// </summary>
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
        }

        /// <summary>
        /// このユーザーの自己紹介文。
        /// </summary>
        public string Biography
        {
            get
            {
                return _biography;
            }
        }

        /// <summary>
        /// このユーザーが公開状態ならTrue。
        /// </summary>
        public bool IsPublic
        {
            get
            {
                return _isPublic;
            }
        }

       



    }
}
