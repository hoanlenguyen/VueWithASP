using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.Enum;
using webapi.Model.Identity;
using webapi.Model.Permission;
using webapi.Model.Products;

namespace webapi.Data
{
    public static class DbInitializer
    {
        internal static async Task Initialize(IdentityDbContext identityDbContext, StoreDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();

            Console.WriteLine("Initializing DB.....");
            List<ProductCategory> categories;
            List<Brand> brands;
            List<Tag> tags;
            Role role;

            if (!(await roleManager.Roles.AnyAsync()))
            {
                role = new Role
                {
                    Name = BaseRoles.Admin,
                    RoleClaims = Permissions.GetAllPermissions()
                                            .Select(p => new RoleClaim { ClaimType = Permissions.Type, ClaimValue = p })
                                            .ToList()
                };
                await roleManager.CreateAsync(role);
            }
            else
            {
                role = await roleManager.FindByNameAsync(BaseRoles.Admin);
            }

            if (!(await identityDbContext.Users.AnyAsync()))
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    UserType = UserType.Admin,
                    UserDetail = new UserDetail
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        DisplayName = "Admin Admin",
                        DateOfBirth = new DateTime(1991, 1, 1),
                    }
                };

                if (role != null)
                {
                    user.UserRoles = new List<UserRole> { new UserRole { RoleId = role.Id } };
                }

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
                await dbContext.SaveChangesAsync();
            }
            else
            {
                categories = await dbContext.ProductCategories.ToListAsync();
            }

            if (!(await dbContext.Brands.AnyAsync()))
            {
                brands = new List<Brand>
                {
                    new Brand { Name = "Nike", Description = "Description of Brand....", IsEnabled = true },
                    new Brand { Name = "H&M", Description = "Description of Brand....", IsEnabled = true },
                    new Brand { Name = "Gucci", Description = "Description of Brand....", IsEnabled = true },
                };
                await dbContext.AddRangeAsync(brands);
                await dbContext.SaveChangesAsync();
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
                await dbContext.SaveChangesAsync();
            }
            else
            {
                tags = await dbContext.Tags.AsNoTracking().ToListAsync();
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
                await dbContext.SaveChangesAsync();
            }

            Console.WriteLine("Initialized DB completely.....");
        }
    }
}