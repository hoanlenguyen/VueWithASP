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
            app.MapGet("product", [AllowAnonymous] async ([FromServices] ApplicationDbContext db) =>
            {
                var products = db.Products.ProjectToType<ProductDTO>();

                return Results.Ok(products);
            });
        }
    }
}