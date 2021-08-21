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



        public IActionResult Input()
        {
            return View();
        }

        public IActionResult Result()
        {
            return View();
        }


    }
}
