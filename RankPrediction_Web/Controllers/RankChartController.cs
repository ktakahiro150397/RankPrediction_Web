using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RankPrediction_Web.Models.ViewModels.Chart;
using RankPrediction_Web.Models.ViewModels;
using RankPrediction_Web.Models.DbContexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RankPrediction_Web.Controllers
{
    public class RankChartController : Controller
    {

        private readonly RankPredictionContext _context;

        public RankChartController(RankPredictionContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            var vm = new ChartIndexViewModel(_context);
            return View(vm);
        }

        public IActionResult ChartTest()
        {
            return View(new LayoutViewModel());
        }

        public IActionResult ajaxTest()
        {
            return View(new LayoutViewModel());
        }

        public IActionResult ajaxTime()
        {
            return Json(new { ret = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") });
        }


    }
}
