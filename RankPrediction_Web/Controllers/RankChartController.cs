using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RankPrediction_Web.Models.ViewModels.Chart;
using RankPrediction_Web.Models.ViewModels;
using RankPrediction_Web.Models.DbContexts;
using RankPrediction_Web.Models.Charts;

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

        public IActionResult ajaxChart(string param)
        {


            //試しにやってみる
            var chartData = new ChartJsData();



            if (param == "1")
            {

                chartData.Config.Data.Labels = new List<string>()
                {
                    "This","Is","param=1"
                };

                chartData.Config.Data.DataSets = new List<DataSetItem>() {
                    new DataSetItem()
                    {
                        Data = new List<int>()
                        {
                            1,2,3
                        }
                    }
                };

            }
            else
            {
                chartData.Config.Data.Labels = new List<string>()
                {
                    "This","Is","param=2"
                };

                chartData.Config.Data.DataSets = new List<DataSetItem>() {
                    new DataSetItem()
                    {
                        Data = new List<int>()
                        {
                            3,4,2
                        }
                    }
                };

                chartData.Config.Options = new ChartConfigOption(chartData.Config.Data.DataSets[0].Data);
            }

            //            string json = //get some json from your DB
            //return new ContentResult { Content = json, ContentType = "application/json" };
            return new ContentResult
            {
                Content = ((IChartData)chartData).GetChartConfigResponse(),
                ContentType = "application/json"
            };
        }


    }
}
