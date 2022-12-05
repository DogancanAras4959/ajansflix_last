using ajansflix.Core;
using ajansflix.Core.AuthorizationHandler;
using ajansflix.CORE.EmailConfig;
using ajansflix.DOMAIN;
using ajansflix.SERVICES.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace ajansflix
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(options => { options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()); });

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddDbContextDI(_configuration, Environment);
            services.AddInjections();

            services.AddAuthentication("FormAuthenticationWithCookie")
             .AddCookie("FormAuthenticationWithCookie", config =>
               {
                   config.Cookie.Name = "FormAuthenticationWithCookie";
                   config.LoginPath = new PathString("/anasayfa/girisyap");
                   config.AccessDeniedPath = "/anasayfa/girisyap";

               });

            //for authorization
            services.AddAuthorization(config =>
            {
                config.AddPolicy("UserPolicy", policyBuilder =>
                {
                    policyBuilder.UserRequireCustomClaim(ClaimTypes.Email);
                    policyBuilder.UserRequireCustomClaim(ClaimTypes.Name);
                });

            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GeneralMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<EmailConfiguration>(_configuration.GetSection("EmailConfiguration"));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/hata/404");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(30),
                    };

                    headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30));
                }
            });

            app.UseStatusCodePagesWithReExecute("/anasayfa/hata/{0}");
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=anasayfa}/{action=sayfa}/{id?}");

                endpoints.MapControllerRoute(
                name: "detay",
                pattern: "/detay/{id}/{productname}", new { controller = "hizmet", action = "detay" });

                endpoints.MapAreaControllerRoute(
                name: "admin",
                areaName: "admin",
                pattern: "admin/{controller=editor}/{action=home}/{id?}");

            });

            app.ApplicationServices
              .CreateScope()
              .ServiceProvider
              .GetRequiredService<ajansflixdbcontext>()
              .Database
              .Migrate();
        }
    }
}
