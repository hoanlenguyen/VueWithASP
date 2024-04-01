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
            //dbContext.Database.Migrate();

            Console.WriteLine("Initializing DB.....");
            List<ProductCategory> categories;
            List<Brand> brands;
            List<Tag> tags;

            if (!(await dbContext.Users.AnyAsync()))
            {
                var user = new User
                {
                    UserName = "admin",
                    Name = "ADMIN",
                    Email = "admin@gmai.com",
                    UserType = UserType.SuperAdmin
                };
                var result = await userManager.CreateAsync(user, "123qwe!@#QWE");
            }

            if (!(await dbContext.ProductCategories.AnyAsync()))
            {
                categories = new List<ProductCategory>
                {
                    new ProductCategory { Name = "SLEEPING BEDS", Description = "Description of ...."},
                    new ProductCategory { Name = "LOUNGE CHAIRS", Description = "Description of ...."},
                    new ProductCategory { Name = "OFFICE CHAIRS", Description = "Description of ...."},
                    new ProductCategory { Name = "TABLES NIGHTSTANDS", Description = "Description of ...."},
                    new ProductCategory { Name = "KITCHEN FURNITURE", Description = "Description of ...."},
                };
                await dbContext.AddRangeAsync(categories);
                dbContext.SaveChanges();
            }
            else
            {
                categories = await dbContext.ProductCategories.ToListAsync();
            }

            if (!(await dbContext.Brands.AnyAsync()))
            {
                brands = new List<Brand>
                {
                    new Brand { Name = "Nike", Description = "Description of Brand...."},
                    new Brand { Name = "H&M", Description = "Description of Brand...."},
                    new Brand { Name = "Gucci", Description = "Description of Brand...."},
                };
                await dbContext.AddRangeAsync(brands);
                dbContext.SaveChanges();
            }
            else
            {
                brands = await dbContext.Brands.ToListAsync();
            }

            if (!(await dbContext.Tags.AnyAsync()))
            {
                tags = new List<Tag>
                {
                    new Tag { Name = "SPRING" },
                    new Tag { Name = "SUMMER"},
                    new Tag { Name = "AUTUMN"},
                    new Tag { Name = "WINTER"},
                };
                await dbContext.AddRangeAsync(tags);
                dbContext.SaveChanges();
            }
            else
            {
                tags = await dbContext.Tags.ToListAsync();
            }

            if (!(await dbContext.Products.AnyAsync()))
            {
                var radom = new Random();
                var products = new List<Product>();
                for (var i = 0; i < 15; i++)
                {
                    products.Add(new Product
                    {
                        Name = $"Product {i + 1}",
                        CategoryId = categories[radom.Next(categories.Count - 1)].Id,
                        BrandId = brands[radom.Next(brands.Count - 1)].Id,
                        Description = $"Description of Product {i + 1}....",
                        AvatarUrl = $"c{i + 1}.png",
                        Price = (decimal)radom.Next(100, 10000) / 100,
                        ProductTags = new List<ProductTag>
                        {
                            new ProductTag { TagId = tags[radom.Next(0,1)].Id },
                            new ProductTag { TagId = tags[radom.Next(2,3)].Id },
                        }
                    });
                }
                await dbContext.AddRangeAsync(products);
                dbContext.SaveChanges();
            }

            Console.WriteLine("Initialized DB completely.....");
        }
    }
}