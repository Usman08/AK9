using AK9.DAL.EntityModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AK9.DAL.EntityModel.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(e => e.BannerImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.CreateDate).HasColumnType("datetime");

            builder.Property(e => e.DetailedDescription).IsUnicode(false);

            builder.Property(e => e.Heading)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ServiceName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ShortDescription)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.UpdateDate).HasColumnType("datetime");

            builder.HasOne(d => d.ServiceIcon)
                .WithMany(p => p.Service)
                .HasForeignKey(d => d.ServiceIconId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_ServiceIcon");
        }
    }
}
