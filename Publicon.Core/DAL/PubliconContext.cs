using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Publicon.Core.DAL.Initializers;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Entities.Configuration;
using Publicon.Core.Modules;
using System.Linq;

namespace Publicon.Core.DAL
{
    public class PubliconContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationField> PublicationFields { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public PubliconContext(DbContextOptions<PubliconContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            SaveChanges();

            if (!Users.Any())
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddPasswordHasherModule();
                var serviceProvider = serviceCollection.BuildServiceProvider();
                Users.Add(new AdministratorInitializer(serviceProvider).Create());
                SaveChanges();
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new FieldConfiguration());
            builder.ApplyConfiguration(new NotificationConfiguration());
            builder.ApplyConfiguration(new PublicationConfiguration());
            builder.ApplyConfiguration(new PublicationFieldConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
