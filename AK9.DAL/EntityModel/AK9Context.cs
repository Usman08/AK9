using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AK9.DAL.EntityModel.Entities;
using AK9.DAL.EntityModel.Configurations;

namespace AK9.DAL.EntityModel
{
    public class AK9Context : IdentityDbContext<User, Role, int>
    {
        public AK9Context()
        {
        }

        public AK9Context(DbContextOptions<AK9Context> options) : base(options)
        {
        }

        public virtual DbSet<Certification> Certification { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceIcon> ServiceIcon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CertificationConfiguration());
            modelBuilder.ApplyConfiguration(new PolicyConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceIconConfiguration());

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "AspNetRole");
                entity.Property(e => e.Id).HasColumnName("AspNetRoleId");
                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("AspNetUserClaim");
                entity.Property(e => e.UserId).HasColumnName("AspNetUserId");
                entity.Property(e => e.Id).HasColumnName("AspNetUserClaimId");
                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("AspNetUserLogin");
                entity.Property(e => e.UserId).HasColumnName("AspNetUserId");
                entity.HasKey(e => e.LoginProvider);
                entity.HasKey(e => e.ProviderKey);

            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("AspNetRoleClaim");
                entity.Property(e => e.Id).HasColumnName("AspNetRoleClaimId");
                entity.Property(e => e.RoleId).HasColumnName("AspNetRoleId");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("AspNetUserRole");
                entity.Property(e => e.UserId).HasColumnName("AspNetUserId");
                entity.Property(e => e.RoleId).HasColumnName("AspNetRoleId");
                entity.HasKey(e => new { e.UserId, e.RoleId });

            });


            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("AspNetUserToken");
                entity.Property(e => e.UserId).HasColumnName("AspNetUserId");
                entity.HasKey(e => e.LoginProvider);

            });
        }
    }
}
