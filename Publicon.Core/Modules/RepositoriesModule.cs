using Microsoft.Extensions.DependencyInjection;
using Publicon.Core.Repositories.Abstract;
using System.Reflection;

namespace Publicon.Core.Modules
{
    public static class RepositoriesModule
    {
        public static void AddRepositoriesModule(this IServiceCollection services)
        {
            var assembly = typeof(RepositoriesModule)
                .GetTypeInfo()
                .Assembly;

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
