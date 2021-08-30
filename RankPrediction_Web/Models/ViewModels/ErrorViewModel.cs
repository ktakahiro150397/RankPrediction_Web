using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RankPrediction_Web.Models.DbContexts;

namespace RankPrediction_Web.Models.ViewModels
{
    public class ErrorViewModel : LayoutViewModel
    {
        public ErrorViewModel(RankPredictionContext db):base()
        {

            Saying = db.Sayings.FromSqlRaw("SELECT TOP(1) * FROM [ml_predict].[sayings] ORDER BY NEWID() ")
                    .First();
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        public Saying Saying { get; set; }

        public string SayingStr
        {
            get
            {
                return $"\"{Saying.SayingJa}\"";
            }
        }

        public string SayingByStr
        {
            get
            {
                return $"\"{Saying.SayingByJa}\"";
            }
        }


    }
}
