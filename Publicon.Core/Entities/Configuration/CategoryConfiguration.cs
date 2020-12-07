using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicon.Core.Entities.Concrete;

namespace Publicon.Core.Entities.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.IsArchived)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(750);

            builder.HasMany(p => p.Fields)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Publications)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
