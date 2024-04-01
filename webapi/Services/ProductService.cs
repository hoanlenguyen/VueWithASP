using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Model.Production;

namespace webapi.Services
{
    public static class ProductService
    {
        public static void AddProductService(this WebApplication app)
        {
            app.MapGet("products", [AllowAnonymous] async ([FromServices] ApplicationDbContext db) =>
            {
                var products = db.Products.ProjectToType<ProductDTO>();

                return Results.Ok(products);
            });

            app.MapGet("products/{id:int}", [AllowAnonymous] async Task<IResult> ([FromServices] ApplicationDbContext db, int id) =>
            {
                var product = await db.Products
                                .Include(p => p.Category)
                                .Include(p => p.Brand)
                                .Include(p => p.ProductTags)
                                .ThenInclude(pt => pt.Tag)
                                .FirstOrDefaultAsync(p => p.Id == id);
                return product != null ? Results.Ok(product.Adapt<ProductDTO>()) : Results.NotFound();
            });

            app.MapGet("category/{id:int}/products", [AllowAnonymous] async ([FromServices] ApplicationDbContext db, int id) =>
            {
                var products = db.Products.Where(p => p.CategoryId == id).ProjectToType<ProductDTO>();

                return Results.Ok(products);
            });

            app.MapGet("brand/{id:int}/products", [AllowAnonymous] async ([FromServices] ApplicationDbContext db, int id) =>
            {
                var products = db.Products.Where(p => p.BrandId == id).ProjectToType<ProductDTO>();

                return Results.Ok(products);
            });

            app.MapGet("tag/{id:int}/products", [AllowAnonymous] async ([FromServices] ApplicationDbContext db, int id) =>
            {
                var products = db.Products.Where(p => p.ProductTags.Any(pt =>pt.TagId== id)).ProjectToType<ProductDTO>();

                return Results.Ok(products);
            });
        }
    }
}