using eStore.Models;
using eStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore
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
            services.AddControllersWithViews();
            //            services.AddDbContextPool<eStoreContext>(
            //options => options.UseSqlServer(Configuration.GetConnectionString("Cs"))
            //);
            //custom services
            services.AddScoped<IRepositoryIntid<Product>, IproductReposatory>();
            services.AddScoped<IRepositoryIntid<Category>, IcategoryReposatory>();
            services.AddScoped<IRepositoryIntid<ProductOrder>, IcartReposatory>();
            services.AddScoped<IReposatory<AccountUser>, IcustomerReposatory>();
            services.AddScoped<Iuser<ProductOrder>, IcartReposatory>();
            services.AddScoped<IProductinCateg, IproductReposatory>();

            services.AddSession(s => {
                s.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddDbContext<eStoreContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("Cs")));
            services.AddIdentity<AccountUser, IdentityRole>(optios =>
            {
                optios.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<eStoreContext>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//check found cookie

            app.UseAuthorization();//check role

            app.UseSession();//configuration ==>configuratservice

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
