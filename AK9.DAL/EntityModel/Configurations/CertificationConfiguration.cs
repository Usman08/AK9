using AK9.DAL.EntityModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AK9.DAL.EntityModel.Configurations
{
    public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.Property(e => e.CertificationImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.CertificationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.CertificationUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.CreateDate).HasColumnType("datetime");

            builder.Property(e => e.UpdateDate).HasColumnType("datetime");
        }
    }
}
