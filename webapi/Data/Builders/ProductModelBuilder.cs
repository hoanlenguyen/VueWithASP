using Microsoft.EntityFrameworkCore;
using webapi.Model.Products;

namespace webapi.Data.Builders;

public static class ProductModelBuilder
{
    private const string Schema = "Product";
    public static void BuildProductModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.BuildCategoryModel();
        modelBuilder.BuildBrandModel();
        modelBuilder.BuildTagModel();
        modelBuilder.BuildProductModel();
    }

    private static void BuildProductModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTemporalTable("Product", Schema);

        });

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

    private static void BuildCategoryModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTemporalTable("Category", Schema);
        });
    }

    private static void BuildBrandModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand", Schema);
        });
    }

    private static void BuildTagModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag", Schema);
        });

        modelBuilder.Entity<ProductTag>(entity =>
        {
            entity.ToTable("Tag", Schema);
        });
    }
}