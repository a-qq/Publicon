using Microsoft.Extensions.DependencyInjection;
using Publicon.Infrastructure.Managers;
using Publicon.Infrastructure.Managers.Abstract;
using Publicon.Infrastructure.Managers.Concrete;
using System.Reflection;

namespace Publicon.Infrastructure.Modules
{
    public static class ManagersModule
    {
        public static void AddManagersModule(this IServiceCollection services)
        {
            var assembly = typeof(ManagersModule)
                .GetTypeInfo()
                .Assembly;

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<IManager>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddSingleton<IBlobManager, BlobManager>();
        }
    }
}
