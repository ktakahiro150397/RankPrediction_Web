using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RankPrediction_Web.Controllers
{
    public class PlayerDatumController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //return View("./Views/PlayerDatum/Index.cshtml");
            //return "This is PlayerDatumController / Index!";
        }

        public string DataForm(int inputVal,string inputStr, int id = -1)
        {
            return HtmlEncoder.Default.Encode($"This is PlayerDatumController / DataInputForm! : id = {id} / inputVal  = {inputVal} / inputStr = {inputStr} ");
        }

        public IActionResult Notice()
        {

            var msgList = new List<string>() {
                "Place message here.",
                "Your Result may vary from actual result.",
                " (result matigaetemo yurusite ne <3)",
                "This message is element of IList<string>!",
                "Razor is sugoi"};

            ViewData["Message"] = msgList;

            return View();
        }
    }
}
