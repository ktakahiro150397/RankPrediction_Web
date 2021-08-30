using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RankPrediction_Web.Models.DbContexts;

namespace RankPrediction_Web.Models.ViewModels
{

    /// <summary>
    /// 戦績データ入力に必要なデータを全て持つViewModel
    /// </summary>
    public class PredictionDataInputViewModel : LayoutViewModel
    {

        public PredictionDataInputViewModel() : base()
        {

        }

        [Required(ErrorMessage = "{0}を選択してください。")]
        [Display(Name = "シーズン名")]
        public int? SelectedSeasonId { get; set; }

        [Required(ErrorMessage = "到達ランクを選択してください。")]
        [Display(Name = "シーズン中最高到達ランク")]
        public int? SelectedRankId { get; set; }

        [Required(ErrorMessage = "{0}を入力してください。")]
        [Range(0, 99.99, ErrorMessage = "{0}は{1}から{2}の範囲で入力してください。")]
        [Display(Name = "K/D")]
        public double? KillDeathRatio { get; set; }

        [Required(ErrorMessage = "{0}を入力してください。")]
        [Range(0, 99999.99, ErrorMessage = "{0}は{1}から{2}の範囲で入力してください。")]
        [Display(Name = "平均ダメージ")]
        public double? AverageDamage { get; set; }

        [Display(Name = "合計ゲーム数")]
        [MatchCountsValidation]
        public string MatchCounts { get; set; }

        /// <summary>
        /// 入力されたマッチ数を整数に変換して返します。
        /// </summary>
        public long MatchCount_long
        {
            get
            {
                if(IsInputMatchCounts == false)
                {
                    return -1;
                }

                if (MatchCounts == null)
                {
                    return -1;
                }

                var matchCountsRegEx = new System.Text.RegularExpressions.Regex(@"^\d+?(|[KkMm]|\.\d{1,3}[KkMm])$");

                if (matchCountsRegEx.IsMatch(MatchCounts))
                {
                    double matchCnt;

                    if (MatchCounts.EndsWith("k") || MatchCounts.EndsWith("K"))
                    {
                        //K付き：
                        matchCnt = Convert.ToDouble(MatchCounts.Substring(0, MatchCounts.Length - 1));
                        matchCnt *= 1000;
                    }
                    else if (MatchCounts.EndsWith("m") || MatchCounts.EndsWith("M"))
                    {
                        //M付き：
                        matchCnt = Convert.ToDouble(MatchCounts.Substring(0, MatchCounts.Length - 1));
                        matchCnt *= 1000000;
                    }
                    else
                    {
                        //サフィックスなし
                        matchCnt = Convert.ToDouble(MatchCounts);
                    }

                    return Convert.ToInt64(matchCnt);
                
                } else
                {
                    return -1;
                }

               

            }
        }

        [Display(Name = "ゲーム数を入力する")]
        public bool IsInputMatchCounts { get; set; }

        [Display(Name = "主にパーティでプレイした")]
        public bool IsParty { get; set; }

        public IEnumerable<SeasonName> SeasonOptions { get; set; }

        public IEnumerable<Rank> RankOptions { get; set; }

    }

    public class MatchCountsValidationAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var input = (string)value;
            var vm = (PredictionDataInputViewModel)validationContext.ObjectInstance;

            if (vm.IsInputMatchCounts)
            {
                if (String.IsNullOrEmpty(input))
                {
                    return new ValidationResult(
                        @"合計ゲーム数を入力してください。",
                        new[] { nameof(vm.MatchCounts) });
                }

                var regEx = new Regex(@"^\d+?(|[KkMm]|\.\d{1,3}[KkMm])$");
                if (!regEx.IsMatch(input))
                {
                    //フォーマット不一致
                    return new ValidationResult(
                       $"合計ゲーム数には数値を入力してください。(\"K\"や\"M\"が末尾についている場合、そのまま入力してください)");
                }

                return ValidationResult.Success;
            }
            else
            {
                //入力しない場合は常に検証成功
                return ValidationResult.Success;
            }

        }
    }





}
