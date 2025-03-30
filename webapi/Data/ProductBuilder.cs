using Microsoft.EntityFrameworkCore;
using webapi.Model.Product;

namespace webapi.Data
{
    public static class ProductBuilder
    {
        public static void BuildProductModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTemporalTable("Products", "Store");
            modelBuilder.Entity<Brand>().ToTemporalTable("Brands", "Store");
            modelBuilder.Entity<Tag>().ToTable("Tags", "Store");
            modelBuilder.Entity<ProductCategory>().ToTemporalTable("ProductCategories", "Store");

            modelBuilder.Entity<ProductTag>().ToTable("ProductTags", "Store");

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
                        .HasMany(e => e.ProductTags)
                        .WithOne(e => e.Product)
                        .HasForeignKey(e => e.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                        .HasMany(e => e.ProductTags)
                        .WithOne(e => e.Tag)
                        .HasForeignKey(e => e.TagId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductTag>().HasKey(x => new { x.ProductId, x.TagId });
        }
    }
}
