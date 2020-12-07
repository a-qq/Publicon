using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicon.Core.Entities.Concrete;

namespace Publicon.Core.Entities.Configuration
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.IsRequired)
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Fields)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            builder.HasMany(p => p.PublicationFields)
                .WithOne(p => p.Field)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
