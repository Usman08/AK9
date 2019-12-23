using AK9.DAL.EntityModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AK9.DAL.EntityModel.Configurations
{
    public class ServiceIconConfiguration : IEntityTypeConfiguration<ServiceIcon>
    {
        public void Configure(EntityTypeBuilder<ServiceIcon> builder)
        {
            builder.Property(e => e.ServiceIconId).ValueGeneratedNever();

            builder.Property(e => e.ServiceIconName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasData(new ServiceIcon { ServiceIconId = 1, ServiceIconName = "flaticon-cctv" },
                            new ServiceIcon { ServiceIconId = 2, ServiceIconName = "flaticon-safe-box" },
                            new ServiceIcon { ServiceIconId = 3, ServiceIconName = "flaticon-internet" },
                            new ServiceIcon { ServiceIconId = 4, ServiceIconName = "flaticon-security" },
                            new ServiceIcon { ServiceIconId = 5, ServiceIconName = "flaticon-technology" },
                            new ServiceIcon { ServiceIconId = 6, ServiceIconName = "flaticon-alarm" },
                            new ServiceIcon { ServiceIconId = 7, ServiceIconName = "flaticon-lifebuoy" },
                            new ServiceIcon { ServiceIconId = 8, ServiceIconName = "flaticon-cab" });
        }
    }
}
