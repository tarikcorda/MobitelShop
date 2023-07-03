using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeminarskiMobiteli.ViewModel;

namespace SeminarskiMobiteli
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
            services.AddControllersWithViews();
            
                services.AddControllersWithViews();

                services.AddDbContext<Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("connectionpc")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: "cors",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200",
                                                          "http://www.contoso.com").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                  });
            });

            var emailConfig = Configuration
       .GetSection("EmailConfiguration")
       .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddSession();
            services.AddDistributedMemoryCache();
                //services.AddIdentity<AppUser, AppRole>(options =>
                //{
                //    options.User.RequireUniqueEmail = true;
                //    options.Password.RequireDigit = true;
                //    options.Password.RequiredLength = 8;
                //    options.SignIn.RequireConfirmedEmail = true;
                //}).AddEntityFrameworkStores<Context>()
                //.AddDefaultTokenProviders();

                //services.AddPaging(options =>
                //    options.ViewName = "Bootstrap4");
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
            app.UseSession();
            app.UseStaticFiles();
            app.UseCors("cors");
            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //         //pattern: "{area=Admin}{controller=Home}/{action=Index}/{id?}");
            //         pattern: "{area=exists}/{controler=Home}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
