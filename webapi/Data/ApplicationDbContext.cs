using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Model.Identity;

namespace webapi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, RoleClaim, IdentityUserToken<int>>
    {
        public ApplicationDbContext
           (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            

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
                   .HasForeignKey(p => p.RoleId);
        }
    }
}
