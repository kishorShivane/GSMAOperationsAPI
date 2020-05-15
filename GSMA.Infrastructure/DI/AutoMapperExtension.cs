using AutoMapper;
using GSMA.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace GSMA.Infrastructure.DI
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutomapperDependency(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
