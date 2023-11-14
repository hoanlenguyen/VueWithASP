using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;

namespace webapi.Services
{
    public static class ProductService
    {
        public static void AddProductService(this WebApplication app)
        {
            app.MapPost("product", [AllowAnonymous] async ([FromServices] ApplicationDbContext db) =>
            {
                var products = await db.Products./*Include(p => p.Category).*/Select(p => new { p.Id, p.Name,/* Category = p.Category!.Name*/ }).ToListAsync();

                return Results.Ok(products);
            });
        }
    }
}