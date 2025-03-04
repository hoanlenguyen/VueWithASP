using Microsoft.EntityFrameworkCore;
using webapi.Model.Identity;

namespace webapi.Data
{
    public static class IdentityBuilder
    {
        public static void BuildIdentityModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetail>().ToTemporalTable("Users", "Profile");
            modelBuilder.Entity<Role>().ToTemporalTable("Roles", "Identity");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", "Identity");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", "Identity");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", "Identity");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", "Identity");
            modelBuilder.Entity<UserToken>().ToTable("UserTokens", "Identity");

            modelBuilder.Entity<UserDetail>(udb =>
            {
                udb.ToTemporalTable("Users", "Identity");
                udb.Property(u => u.Email).HasColumnName("Email").HasMaxLength(200); //set column name the same in User table 
                udb.Property(u => u.ChangedByUser).HasColumnName("ChangedByUser"); //set column name the same in User table 
            });

            modelBuilder.Entity<User>(ub =>
            {
                ub.ToTemporalTable("Users", "Identity");

                ub.Property(u => u.Email).HasColumnName("Email").HasMaxLength(200);
                ub.Property(u => u.ChangedByUser).HasColumnName("ChangedByUser");

                ub.HasOne(o => o.UserDetail).WithOne()
                    .HasForeignKey<UserDetail>(ud => ud.Id);
                ub.Navigation(o => o.UserDetail).IsRequired();
            });

            modelBuilder.Entity<UserRole>()
                    .HasOne(p => p.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<UserRole>()
                    .HasOne(p => p.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<RoleClaim>()
                   .HasOne(p => p.Role)
                   .WithMany(b => b.RoleClaims)
                   .HasForeignKey(p => p.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}