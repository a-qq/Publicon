using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Publicon.Infrastructure.Modules
{
    public static class MediatRModule
    {
        public static void AddMediatRModule(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
           // services.AddMediatorHandlers()
        }
    }
}
