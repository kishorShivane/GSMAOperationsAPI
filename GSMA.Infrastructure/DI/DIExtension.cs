using AutoMapper;
using GSMA.Core.Interface;
using GSMA.Core.Service;
using GSMA.DataProvider.UnitOfWork;
using GSMA.Logger;
using GSMA.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GSMA.Mapper;

namespace GSMA.Infrastructure.DI
{
    public static class DIExtension
    {
        public static IServiceCollection AddInterfaceDependency(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IService<EGMDetailModel>, EGMDetailsService>();
            services.AddTransient<IService<EGMSealModel>, EGMSealsService>();
            services.AddTransient<IService<SealDetailModel>, SealDetailsService>();
            services.AddTransient<IService<UserTypeModel>, UserTypeService>();
            services.AddTransient<IService<UserModel>, UserService>();
            return services;
        }
    }
}
