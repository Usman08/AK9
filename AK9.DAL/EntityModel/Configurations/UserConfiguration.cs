using AK9.DAL.EntityModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AK9.DAL.EntityModel.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUser");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(i => i.Id).HasColumnName("AspNetUserId");
            builder.Property(i => i.Email).HasMaxLength(256);
            builder.Property(i => i.NormalizedEmail).HasMaxLength(256);
            builder.Property(i => i.UserName).HasMaxLength(256);
            builder.Property(i => i.NormalizedUserName).HasMaxLength(256);
        }
    }
}
