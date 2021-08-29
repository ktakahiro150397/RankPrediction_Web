using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RankPrediction_Web.Models.DbContexts;
using RankPrediction_Web.Models.ViewModels;

namespace RankPrediction_Web.Controllers
{
    public class ErrorController : Controller
    {
        private RankPredictionContext db;

        public ErrorController(RankPredictionContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {

            var vm = new ErrorViewModel(db);

            return View(vm);
        }
    }
}
