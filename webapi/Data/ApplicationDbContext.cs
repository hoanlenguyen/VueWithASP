using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Model.Identity;
using webapi.Model.Production;

namespace webapi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext
           (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region Identity

        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }

        #endregion Identity

        #region Product

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        #endregion Product

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserToken>().ToTable("UserTokens");

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

            modelBuilder.Entity<Product>()
                   .HasOne(p => p.Category)
                   .WithMany(b => b.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                   .HasOne(p => p.Brand)
                   .WithMany(b => b.Products)
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                        .HasMany(e => e.Tags)
                        .WithMany(e => e.Products)
                        .UsingEntity<ProductTag>();
        }
    }
}