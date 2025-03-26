using Microsoft.EntityFrameworkCore;
using webapi.Data.Builders.Extensions;
using webapi.Model.Identity;

namespace webapi.Data.Builders;

public static class IdentityModelBuilder
{
    private const string IdentitySchema = "Identity";
    public static void BuildIdentityModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.BuildUserModel();



        modelBuilder.Entity<Role>().ToTable("Roles", IdentitySchema);
        modelBuilder.Entity<UserRole>().ToTable("UserRoles", IdentitySchema);
        modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", IdentitySchema);
        modelBuilder.Entity<UserClaim>().ToTable("UserClaims", IdentitySchema);
        modelBuilder.Entity<UserLogin>().ToTable("UserLogins", IdentitySchema);
        modelBuilder.Entity<UserToken>().ToTable("UserTokens", IdentitySchema);

        modelBuilder.Entity<UserRole>()
                .HasOne(p => p.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<UserRole>()
                .HasOne(p => p.Role)
                .WithMany()
                .HasForeignKey(p => p.RoleId);

        modelBuilder.Entity<RoleClaim>()
               .HasOne(p => p.Role)
               .WithMany(b => b.RoleClaims)
               .HasForeignKey(p => p.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
    private static void BuildUserModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTemporalTable("Users", IdentitySchema);

            entity.HasIndex(e => e.NormalizedEmail, "IX_Identity_User_NormalizedEmail");

            entity.HasIndex(e => e.NormalizedUserName, "IX_Identity_User_NormalizedUserName")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");
        });
    }
}