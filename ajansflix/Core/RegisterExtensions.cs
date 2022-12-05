using ajansflix.CORE.Repository;
using ajansflix.DOMAIN;
using ajansflix.SERVICES.Interface;
using ajansflix.SERVICES.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ajansflix.Core
{
    internal static class RegisterExtensions
    {
        internal static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var contextConnectionString = configuration.GetConnectionString("Default");
            services.AddDbContextPool<ajansflixdbcontext>(x => x.UseSqlServer(contextConnectionString, o =>
            {
                o.EnableRetryOnFailure(3);
            })
                .EnableSensitiveDataLogging(environment.IsDevelopment())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        internal static void AddInjections(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IDetailService, DetailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IDetailDataService, DetailDataService>();
            services.AddTransient<IProductDetailService, ProductDetailService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IOrderProductsService, OrderProductService>();
            services.AddTransient<IVideoDataService, VideoDataService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
        }
    }
}
