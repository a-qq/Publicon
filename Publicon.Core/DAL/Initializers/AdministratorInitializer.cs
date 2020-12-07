using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Publicon.Core.Entities.Concrete;
using System;

namespace Publicon.Core.DAL.Initializers
{
    public class AdministratorInitializer
    {
        public readonly IPasswordHasher<User> _passwordHasher;
        public readonly IServiceProvider _serviceProvider;

        public AdministratorInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _passwordHasher = _serviceProvider.GetService<IPasswordHasher<User>>();
        }

        public User Create()
        {
            var admin = new User(Environment.GetEnvironmentVariable("AdminEmail"), "Publicon", "Administrator");
            admin.SetHashedPassword(_passwordHasher.HashPassword(admin, Environment.GetEnvironmentVariable("AdminPassword")));
            admin.ActivateAccount();

            return admin;
        }
    }
}
