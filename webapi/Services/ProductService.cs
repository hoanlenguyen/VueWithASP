using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Helper;
using webapi.Model.Product;
using webapi.Paging;

namespace webapi.Services
{
    public static class ProductService
    {
        public static void AddProductService(this WebApplication app)
        {
            app.MapGet("products", [AllowAnonymous] async ([FromServices] ApplicationDbContext db, [AsParameters] ProductFilter request) =>
            {
                var query = db.Products
                            .Include(p => p.Category)
                            .Include(p => p.Brand)
                            .Include(p => p.ProductTags)
                            .ThenInclude(pt => pt.Tag)
                            .AsNoTracking()
                            .WhereIf(request.Name.IsNotNullOrEmpty(), p => p.Name.Contains(request.Name!))
                            .WhereIf(request.CategoryId.HasValue, p => p.CategoryId == request.CategoryId)
                            .WhereIf(request.BrandId.HasValue, p => p.BrandId == request.BrandId)
                            .WhereIf(request.PriceFrom.HasValue, p => p.Price >= request.PriceFrom)
                            .WhereIf(request.PriceTo.HasValue, p => p.Price <= request.PriceTo)
                            ;

                var totalCount = await query.CountAsync();


                var items = totalCount > 0 ?
                                await query.OrderAndPaging(request).ProjectToType<ProductDTO>().ToListAsync() :
                                new List<ProductDTO>();

                return Results.Ok(new PagedResultDto<ProductDTO>(totalCount, items));
            });

            app.MapGet("userdetail", [AllowAnonymous] async ([FromServices] ApplicationDbContext db) =>
            {
                var query = await db.UserDetails.FirstOrDefaultAsync();

                return Results.Ok(query);
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
                var products = db.Products.Where(p => p.ProductTags.Any(pt => pt.TagId == id)).ProjectToType<ProductDTO>();

                return Results.Ok(products);
            });


            app.MapPut("products", [AllowAnonymous] async Task<IResult> ([FromServices] ApplicationDbContext db, [FromBody] ProductDTO model) =>
            {
                var productEntity = await db.Products
                                .Include(p => p.ProductTags)
                                .FirstOrDefaultAsync(p => p.Id == model.Id);

                if (productEntity is null)
                {
                    return Results.NotFound();
                }

                productEntity.ProductTags.Clear();
                model.Adapt(productEntity);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            app.MapPost("products", [AllowAnonymous] async Task<IResult> ([FromServices] ApplicationDbContext db, [FromBody] ProductDTO model) =>
            {
                model.Id = default;
                var isExisted = await db.Products.AnyAsync(p => p.Name == model.Name && p.Id != model.Id);
                if (isExisted)
                {
                    throw new Exception($"{model.Name} existed");
                }
                var productEntity = model.Adapt<Product>();
                await db.Products.AddAsync(productEntity);
                await db.SaveChangesAsync();
                model.Id = productEntity.Id;
                return Results.Ok(model);
            });

            app.MapDelete("products/{id:int}", [AllowAnonymous] async Task<IResult> ([FromServices] ApplicationDbContext db, int id) =>
            {
                var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    return Results.NotFound();
                }
                db.Products.Remove(product!);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}