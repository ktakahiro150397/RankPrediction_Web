using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RankPrediction_Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using RankPrediction_Web.Models.DbContexts;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace RankPrediction_Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

#if DEBUG
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
#else
            services.AddControllersWithViews();
#endif

            services.AddDbContext<RankPredictionContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("dbml")));

            //外部認証Configurationの追加
            services.AddAuthentication()
                    .AddSteam(options =>
                    {
                        options.CallbackPath = new Microsoft.AspNetCore.Http.PathString("/Login/Welcome");
                        options.ApplicationKey = "A03F81037033B61CEAC131267532DA07";
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ApexRank}/{action=Index}/{id?}");
            });

            loggerFactory.AddProvider(new SystemLoggerProvider());
        }
    }
}
