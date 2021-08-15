using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RankPrediction_Web.Models
{

    /// <summary>
    /// 戦績データ入力に必要なデータを全て持つViewModel
    /// </summary>
    public class PredictionDataInputViewModel
    {

        [Required(ErrorMessage = "{0}を選択してください。")]
        [Display(Name ="シーズン名")]
        public int? SelectedSeasonId { get; set; }

        [Required(ErrorMessage = "到達ランクを選択してください。")]
        [Display(Name = "最高到達ランク")]
        public int? SelectedRankId { get; set; }

        [Required(ErrorMessage = "{0}を入力してください。")]
        [Range(0, 99.99, ErrorMessage = "{0}は{1}から{2}の範囲で入力してください。")]
        [Display(Name = "K/D")]
        public decimal? KillDeathRatio { get; set; }

        [Required(ErrorMessage = "{0}を入力してください。")]
        [Range(0, 99999.99, ErrorMessage = "{0}は{1}から{2}の範囲で入力してください。")]
        [Display(Name = "平均ダメージ")]
        public decimal? AverageDamage { get; set; }

        [Required(ErrorMessage = "{0}を入力してください。")]
        [Display(Name = "合計ゲーム数")]
        public int? MatchCounts { get; set; }

        [Display(Name = "主にパーティでプレイした")]
        public bool IsParty { get; set; }

        public IEnumerable<SeasonName> SeasonOptions { get; set; }

        public IEnumerable<Rank> RankOptions { get; set; }

    }
}
