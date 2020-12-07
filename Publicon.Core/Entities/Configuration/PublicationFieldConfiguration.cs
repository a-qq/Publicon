using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicon.Core.Entities.Concrete;

namespace Publicon.Core.Entities.Configuration
{
    public class PublicationFieldConfiguration : IEntityTypeConfiguration<PublicationField>
    {
        public void Configure(EntityTypeBuilder<PublicationField> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value)
                .IsRequired();

            builder.HasOne(p => p.Field)
                .WithMany(p => p.PublicationFields)
                .HasForeignKey(p => p.FieldId)
                .IsRequired();

            builder.HasOne(p => p.Publication)
                .WithMany(p => p.PublicationFields)
                .HasForeignKey(p => p.PublicationId)
                .IsRequired(); 
        }
    }
}
