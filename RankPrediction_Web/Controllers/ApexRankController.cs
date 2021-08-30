using Microsoft.AspNetCore.Mvc;
using RankPrediction_Web.Models;
using RankPrediction_Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RankPrediction_Web.Models.DbContexts;

namespace RankPrediction_Web.Controllers
{
    public class ApexRankController : Controller
    {
        private readonly RankPredictionContext _context;

        public ApexRankController(RankPredictionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }


        [HttpGet]
        public IActionResult Input()
        {

            var vm = new PredictionDataInputViewModel
            {
                SeasonOptions = _context.SeasonNames.OrderBy(item => item.DisplaySeq),
                RankOptions = _context.Ranks.OrderBy(item => item.DisplaySeq),
                IsInputMatchCounts = true
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Input([Bind("SelectedSeasonId," +
            "SelectedRankId",
            "KillDeathRatio",
            "AverageDamage",
            "MatchCounts",
            "IsParty",
            "IsInputMatchCounts")] PredictionDataInputViewModel bindVm)
        {
            if (ModelState.IsValid)
            {
                var addPrediction = new PredictionDatum()
                {
                    SeasonId = bindVm.SelectedSeasonId.Value,
                    RankId = bindVm.SelectedRankId.Value,
                    KillDeathRatio = bindVm.KillDeathRatio.Value,
                    AverageDamage = bindVm.AverageDamage.Value,
                    IsParty = bindVm.IsParty,
                    MatchCounts = bindVm.MatchCount_long
                };

                _context.Add(addPrediction);
                await _context.SaveChangesAsync();

                //インサートしたデータのIDを取得
                var insertId = addPrediction.Id;

                return RedirectToAction("Calc", "ApexRank",new { id = insertId });

            }
            else
            {
                var vm = new PredictionDataInputViewModel
                {
                    SeasonOptions = _context.SeasonNames.OrderBy(item => item.DisplaySeq),
                    RankOptions = _context.Ranks.OrderBy(item => item.DisplaySeq),
                    IsInputMatchCounts = bindVm.IsInputMatchCounts
                };

                return View(vm);
            }
        }

        public IActionResult Calc(int? id)
        {

#if DEBUG
#else
            //URLパラメータが存在しない場合トップ画面へ
            if(!id.HasValue){
                return View("Index", "ApexRank");
            }
#endif

            var vm = new CalcViewModel(_context);
            if (id.HasValue)
            {
                vm.DataId = id.Value;
            }

            return View(vm);
        }



        public IActionResult Result(int? id)
        {

            //URLパラメータが存在しない場合トップページに戻す
            if(!id.HasValue)
            {
                return View("Index", "ApexRank");
            }

            //指定IDの予測結果VMを取得
            var resultVm = new PredictionResultViewModel(_context, id.Value);


            if(resultVm.PredictedResult.PredictResult == null)
            {
                //存在しないID・予測に失敗している場合はトップに戻す
                return View("Index", "ApexRank");
            }
            return View(resultVm);
        }


    }
}
