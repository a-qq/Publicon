using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicon.Core.Entities.Concrete;

namespace Publicon.Core.Entities.Configuration
{
    public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.AddedAt)
                .IsRequired();

            builder.Property(p => p.FileName)
                .HasMaxLength(200);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Publications)
                .HasForeignKey(p => p.UserId);

            builder.HasMany(p => p.PublicationFields)
                .WithOne(p => p.Publication)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Publications)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();
        }
    }
}
