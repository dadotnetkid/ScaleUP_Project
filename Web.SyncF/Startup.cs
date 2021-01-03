using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Syncfusion.Licensing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web.SyncF.Data;

namespace Web.SyncF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            SyncfusionLicenseProvider.RegisterLicense(
                "MDAxQDMxMzgyZTM0MmUzMGdVTXdwditJbDRRYjQ0QXpRL2xXOTFpZk0zcWxVeTUySW5Fc0g4MDlQbWM9");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            /*services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.LoginPath = "/members/login";
                opt.ExpireTimeSpan = new TimeSpan(8, 0, 0);
                opt.type
            });*/

            /*services
                .AddAuthentication(opt =>
                {
                    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/members/login";
                    options.LoginPath = "/members/logout";
                });*/
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/members/login";
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employees}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"node_modules", @"@syncfusion")))
            {
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"js", @"ej2")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"js", @"ej2"));
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules", @"@syncfusion", @"ej2-js-es5",
                            @"scripts", @"ej2.min.js"),
                        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"js", @"ej2", @"ej2.min.js"));
                }

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css",
                        @"ej2"));
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules", @"@syncfusion", @"ej2-js-es5",
                            @"styles", @"bootstrap.css"),
                        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"bootstrap.css"));
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules", @"@syncfusion", @"ej2-js-es5",
                            @"styles", @"bootstrap4.css"),
                        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"bootstrap4.css"));
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules", @"@syncfusion", @"ej2-js-es5",
                            @"styles", @"material.css"),
                        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"material.css"));
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules", @"@syncfusion", @"ej2-js-es5",
                            @"styles", @"highcontrast.css"),
                        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"highcontrast.css"));
                    File.Copy(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules", @"@syncfusion", @"ej2-js-es5",
                            @"styles", @"fabric.css"),
                        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"fabric.css"));
                }
            }
        }
    }
}