using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Publicon.Core.Entities.Concrete;

namespace Publicon.Core.Modules
{
    public static class PasswordHasherModule
    {
        public static void AddPasswordHasherModule(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }
    }
}
