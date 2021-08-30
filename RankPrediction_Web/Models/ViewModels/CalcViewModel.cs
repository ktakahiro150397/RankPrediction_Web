using System;
using RankPrediction_Web.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RankPrediction_Web.Models.ViewModels
{
    public class CalcViewModel : LayoutViewModel
    {
        public CalcViewModel(RankPredictionContext db) : base()
        {
            Saying = db.Sayings.FromSqlRaw("SELECT TOP(1) * FROM [ml_predict].[sayings] ORDER BY NEWID() ")
                    .First();
        }

        /// <summary>
        /// 結果表示を要求する入力データID
        /// </summary>
        public int? DataId {get;set;}

        public Saying Saying { get; set; }

    }
}
