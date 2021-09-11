using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RankPrediction_Web.Extension
{
    public static class HttpContextExtensions
    {

        /// <summary>
        /// AuthenticationSchemeProviderの一覧を取得する？？拡張メソッド
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<AuthenticationScheme[]> GetExternalProviderAsync(this HttpContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

            var allSchemes = await schemes.GetAllSchemesAsync();

            return allSchemes
                   .Where(item => !string.IsNullOrEmpty(item.DisplayName))
                   .ToArray();

        }

        /// <summary>
        /// 指定のプロバイダ名がサポートされているAuthenticationAchemeProviderに含まれているかを確認する？？メソッド
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static async Task<bool> IsProviderSupportedAsync(this HttpContext context,string provider)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var schemes = await context.GetExternalProviderAsync();

            return schemes.Any(item => string.Equals(item.Name, provider, StringComparison.OrdinalIgnoreCase));

        }


    }
}
