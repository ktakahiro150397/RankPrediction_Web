using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankPrediction_Web.Models.DbContexts;
using RankPrediction_Web_Test.ViewModel;

namespace RankPrediction_Web_Test
{

    /// <summary>
    /// dotnet user-secretsに格納されているConnectionStringで初期化されたDBContextを提供します。
    /// </summary>
    public abstract class DbContextBase
    {
        protected IConfiguration config;
        protected RankPredictionContext db;

        public DbContextBase()
        {
            // Configurationの設定：テスト用User-secretsから接続文字列を読み込む
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<RankPredictionContext>();
            config = builder.Build();

            // DBContextOptionsの設定：指定接続文字列でSQLServerへ接続
            var conOpBuilder = new DbContextOptionsBuilder<RankPredictionContext>()
                .UseSqlServer(config.GetConnectionString("dbml"));

            db = new RankPredictionContext(conOpBuilder.Options);
        }
    }
}

