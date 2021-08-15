using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RankPrediction_Web.Models
{
    public partial class PredictionDatum
    {
        public int Id { get; set; }


        public int SeasonId { get; set; }
        public int RankId { get; set; }

        [Required(AllowEmptyStrings =true,ErrorMessage ="{0}を入力してください。")]
        [Range(0,99.99,ErrorMessage = "{0}は{1}から{2}の範囲で入力してください。")]
        [Display(Name = "K/D値")]
        public decimal KillDeathRatio { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "{0}を入力してください。")]
        [Range(0,99999.99,ErrorMessage = "{0}は{1}から{2}の範囲で入力してください。")]
        [Display(Name = "平均ダメージ")]
        public decimal AverageDamage { get; set; }

        [Display(Name = "合計ゲーム数")]
        public int MatchCounts { get; set; }

        [Display(Name = "PTプレイ")]
        public bool IsParty { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual SeasonName Season { get; set; }
    }
}
