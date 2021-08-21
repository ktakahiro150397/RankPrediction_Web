using Microsoft.AspNetCore.Mvc;
using RankPrediction_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return View();
        }


        [HttpGet]
        public IActionResult Input()
        {

            var vm = new PredictionDataInputViewModel
            {
                SeasonOptions = _context.SeasonNames,
                RankOptions = _context.Ranks
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
            "IsParty")] PredictionDataInputViewModel bindVm)
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

                return RedirectToAction("Result", "ApexRank",new { id = insertId });

            }
            else
            {
                var vm = new PredictionDataInputViewModel
                {
                    SeasonOptions = _context.SeasonNames,
                    RankOptions = _context.Ranks
                };

                return View(vm);
            }
        }

        public IActionResult Result(int? id)
        {

            //URLパラメータが存在しない場合トップに戻る
            if(!id.HasValue)
            {
                return View("Index", "ApexRank");
            }



            return View();
        }


    }
}
