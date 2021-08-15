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

        [Required(ErrorMessage = "シーズン名を選択してください。")]
        [Display(Name ="シーズン名")]
        public int? SelectedSeasonId { get; set; }

        [Required(ErrorMessage = "到達ランクを選択してください。")]
        [Display(Name = "最高到達ランク")]
        public int? SelectedRankId { get; set; }

        public IEnumerable<SeasonName> SeasonOptions { get; set; }

        public IEnumerable<Rank> RankOptions { get; set; }

    }
}
