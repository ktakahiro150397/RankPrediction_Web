using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RankPrediction_Web.Extension;

namespace RankPrediction_Web.Models.ViewModels.Login
{
    public class LoginIndexViewModel : LayoutViewModel
    {
        public LoginIndexViewModel() : base()
        {
        }

        public async Task SetAuthenticationSchemeArrayAsync(HttpContext context)
        {
            AuthenticationSchemes = await context.GetExternalProviderAsync();

        }

        public AuthenticationScheme[] AuthenticationSchemes;

    }
}
