using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Model.NamedModels;

namespace webapi.Services
{
    public static class NameModelService
    {
        public static void AddNameModelService(this WebApplication app)
        {
            app.MapGet("namemodel", [AllowAnonymous] async ([FromServices] StoreDbContext db, [AsParameters] NamedModelRequest request) =>
            {
                var result = await NameModelQuery.GetAsync(db, request);

                return Results.Ok(result);
            });
        }
    }
}