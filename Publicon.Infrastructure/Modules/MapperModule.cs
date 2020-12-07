using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Publicon.Infrastructure.Modules
{
    public static class MapperModule
    {
        public static void AddMapperModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperModule));
        }
    }
}
