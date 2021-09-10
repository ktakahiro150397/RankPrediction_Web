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

        [HttpGet]
        public IActionResult RankToChart()
        {
            return View(new RankToChartViewModel());
        }

        /// <summary>
        /// チャートデータのJsonオブジェクトを返却
        /// </summary>
        /// <param name="chartparam"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RetrieveChartData(string chartparam)
        {

            //パラメータの解析
            //enum.ToStringとchartparamをぶち当てる
            object chartTypeParse;
            if (Enum.TryParse(typeof(ChartDisplayData), chartparam, out chartTypeParse))
            {
                //ChartTypeの取得に成功
                IChartData chartData = new ChartDataRepository(_context).RetrieveChartDataByChartType((ChartDisplayData)chartTypeParse);

                if (chartData == null)
                {
                    //名称が存在しない
                    return View(new RankToChartViewModel());
                }
                else
                {
                    //結果の返却
                    return new ContentResult
                    {
                        Content = chartData.GetChartConfigResponse(),
                        ContentType = "application/json"
                    };
                }
            }
            else
            {
                //名称が存在しない
                return View(new RankToChartViewModel());
            }
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
                        Data = new List<double>()
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
                        Data = new List<double>()
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
