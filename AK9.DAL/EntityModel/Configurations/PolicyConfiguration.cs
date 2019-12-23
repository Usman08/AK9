using AK9.DAL.EntityModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AK9.DAL.EntityModel.Configurations
{
    public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.Property(e => e.CreateDate).HasColumnType("datetime");

            builder.Property(e => e.PolicyFile)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PolicyName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.UpdateDate).HasColumnType("datetime");
        }
    }
}
