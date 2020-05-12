using Abstract;
using DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;

namespace Resolver
{
    public class ServicesRegistator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReaderService, ReaderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExtruderService, ExtruderService>();



        }
    }
}
