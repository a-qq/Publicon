using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicon.Core.Entities.Concrete;

namespace Publicon.Core.Entities.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Message)
                .HasMaxLength(25000)
                .IsRequired();
        }
    }
}
