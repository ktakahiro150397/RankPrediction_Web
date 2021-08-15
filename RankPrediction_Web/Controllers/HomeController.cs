using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RankPrediction_Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RankPrediction_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly RankPredictionContext _dbContext;

        //コントローラーのコンストラクタにDIコンテナからDBContextが注入される？
        public HomeController(ILogger<HomeController> logger,RankPredictionContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            var rankList = _dbContext.Ranks.Select(item => item.RankName);

            return View();
        }

        public IActionResult Notice()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
