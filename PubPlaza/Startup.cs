using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubPlaza.Data.Mocks;
using PubPlaza.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using PubPlaza.Data;
using PubPlaza.Data.Repositories;
using Microsoft.AspNetCore.Http;
using PubPlaza.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace PubPlaza
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
            services.AddDbContext<PubPlazaContext>
                (options => options.UseSqlServer
                (Configuration.GetConnectionString("PubPlazaConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PubPlazaContext>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();   
            services.AddTransient<IDrinkRepository, DrinkRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //two people  can ask for the instance at the same time will get difffernt instances
            services.AddScoped(sp => ShoppingCart.GetCart(sp));
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();
            //Below is doing exactly what it is by above commented code
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "CategoryFilter", template: "Drink/{action}/{Category?}", defaults: new { Controller = "Drink", action = "Index" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

        }
    }
}
