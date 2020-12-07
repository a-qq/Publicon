using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Entities.Enums;

namespace Publicon.Core.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Role)
                .IsRequired();

            builder.Property(p => p.GivenName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.FamilyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.HashedPassword)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.HasMany(p => p.Publications)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
