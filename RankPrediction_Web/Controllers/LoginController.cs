using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RankPrediction_Web.Models.ViewModels;
using RankPrediction_Web.Models.ViewModels.Login;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RankPrediction_Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {

            var vm = new LoginIndexViewModel();

            await vm.SetAuthenticationSchemeArrayAsync(HttpContext);


            return View(vm);
        }


        [HttpPost]
        public IActionResult Index([FromForm] string provider)
        {

            if (string.IsNullOrEmpty(provider))
            {
                return BadRequest();
            }




            return Challenge(
                   new AuthenticationProperties
                   {
                       RedirectUri  = "/"
                   },
                   provider);
        }


    }
}
