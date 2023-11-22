using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.Enum;
using webapi.Model.Identity;
using webapi.Model.Production;

namespace webapi.Data
{
    public static class DbInitializer
    {
        internal static async Task Initialize(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            if (dbContext.Users.Any() && dbContext.Products.Any())
                return;
            Console.WriteLine("Initializing DB.....");
            var user = new User
            {
                UserName = "admin",
                Name = "ADMIN",
                Email = "admin@gmai.com",
                UserType = UserType.SuperAdmin
            };
            var result = await userManager.CreateAsync(user, "123qwe!@#QWE");

            var categories = new List<ProductCategory>
            {
                new ProductCategory { Name = "Cloth", Description = "Description of ...."},
                new ProductCategory { Name = "Shoe", Description = "Description of ...."},
                new ProductCategory { Name = "Shirt", Description = "Description of ...."},
            };
            await dbContext.AddRangeAsync(categories);
            dbContext.SaveChanges();

            var brands = new List<Brand>
            {
                new Brand { Name = "Nike", Description = "Description of Brand...."},
                new Brand { Name = "H&M", Description = "Description of Brand...."},
                new Brand { Name = "Gucci", Description = "Description of Brand...."},
            };
            await dbContext.AddRangeAsync(brands);
            dbContext.SaveChanges();

            var tags = new List<Tag>
            {
                new Tag { Name = "SPRING" },
                new Tag { Name = "SUMMER"},
                new Tag { Name = "AUTUMN"},
            };
            await dbContext.AddRangeAsync(tags);
            dbContext.SaveChanges();

            var products = new List<Product>
            {
                new Product {
                    Name = "Product I",
                    CategoryId = categories[0].Id,
                    BrandId = brands[0].Id,
                    Description = "Description of Product....",
                    ProductTags = new List<ProductTag>
                    {
                        new ProductTag { TagId = tags[0].Id },
                        new ProductTag { TagId = tags[1].Id },
                    }
                },
                new Product {
                    Name = "Product II",
                    CategoryId = categories[1].Id,
                    BrandId = brands[1].Id,
                    Description = "Description of Product....",
                    ProductTags = new List<ProductTag>
                    {
                        new ProductTag { TagId = tags[1].Id },
                        new ProductTag { TagId = tags[2].Id },
                    }
                },
            };
            await dbContext.AddRangeAsync(products);
            dbContext.SaveChanges();
            Console.WriteLine("Initialized DB.....");
        }
    }
}