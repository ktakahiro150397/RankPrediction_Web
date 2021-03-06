using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RankPrediction_Web.Models.ViewModels;
using RankPrediction_Web.Models.DbContexts;

namespace RankPrediction_Web.Controllers
{
    public class PredictionDatumsController : Controller
    {
        private readonly RankPredictionContext _context;

        public PredictionDatumsController(RankPredictionContext context)
        {
            _context = context;
        }

        // GET: PredictionDatums
        public async Task<IActionResult> Index()
        {
            var rankPredictionContext = _context.PredictionData.Include(p => p.Rank).Include(p => p.Season);
            return View(await rankPredictionContext.ToListAsync());
        }

        // GET: PredictionDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predictionDatum = await _context.PredictionData
                .Include(p => p.Rank)
                .Include(p => p.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (predictionDatum == null)
            {
                return NotFound();
            }

            return View(predictionDatum);
        }

        // GET: PredictionDatums/Create
        public IActionResult Create()
        {


            var vm = new PredictionDataInputViewModel
            {
                SeasonOptions = _context.SeasonNames,
                RankOptions = _context.Ranks
            };

            return View(vm);

        }

        // POST: PredictionDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelectedSeasonId,SelectedRankId,KillDeathRatio,AverageDamage,MatchCounts,IsParty")] PredictionDataInputViewModel bindVm)
        {
            if (ModelState.IsValid)
            {
                //Obtain data from posted ViewModel
                var addPrediction = new PredictionDatum();
                addPrediction.SeasonId = bindVm.SelectedSeasonId.Value;
                addPrediction.RankId = bindVm.SelectedRankId.Value;
                addPrediction.KillDeathRatio = bindVm.KillDeathRatio.Value;
                addPrediction.AverageDamage = bindVm.AverageDamage.Value;
                addPrediction.IsParty = bindVm.IsParty;

                //マッチ数
                addPrediction.MatchCounts = bindVm.MatchCount_long;


                //Add data
                _context.Add(addPrediction);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "PredictionDatums");
            }

            // Rebind data when model is invalid.
            var vm = new PredictionDataInputViewModel
            {
                SeasonOptions = _context.SeasonNames,
                RankOptions = _context.Ranks
            };

           return View(vm);

        }

        // GET: PredictionDatums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predictionDatum = await _context.PredictionData.FindAsync(id);
            if (predictionDatum == null)
            {
                return NotFound();
            }
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "RankName", predictionDatum.RankId);
            ViewData["SeasonId"] = new SelectList(_context.SeasonNames, "SeasonId", "SeasonName1", predictionDatum.SeasonId);
            return View(predictionDatum);
        }

        // POST: PredictionDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeasonId,RankId,KillDeathRatio,AverageDamage,MatchCounts,IsParty,CreateDate")] PredictionDatum predictionDatum)
        {
            if (id != predictionDatum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(predictionDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredictionDatumExists(predictionDatum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "RankName", predictionDatum.RankId);
            ViewData["SeasonId"] = new SelectList(_context.SeasonNames, "SeasonId", "SeasonName1", predictionDatum.SeasonId);
            return View(predictionDatum);
        }

        // GET: PredictionDatums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predictionDatum = await _context.PredictionData
                .Include(p => p.Rank)
                .Include(p => p.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (predictionDatum == null)
            {
                return NotFound();
            }

            return View(predictionDatum);
        }

        // POST: PredictionDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predictionDatum = await _context.PredictionData.FindAsync(id);
            _context.PredictionData.Remove(predictionDatum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredictionDatumExists(int id)
        {
            return _context.PredictionData.Any(e => e.Id == id);
        }



    }
}
