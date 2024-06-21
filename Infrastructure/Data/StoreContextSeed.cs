using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext dbContext)
        {
            
            if (!dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands ON");
                        await dbContext.ProductBrands.AddRangeAsync(brands);
                        await dbContext.SaveChangesAsync();
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands OFF");
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands OFF");
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }

            if (!dbContext.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes ON");
                        await dbContext.ProductTypes.AddRangeAsync(types);
                        await dbContext.SaveChangesAsync();
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes OFF");
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes OFF");
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

            }

            if (!dbContext.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                dbContext.Products.AddRange(products);
                await dbContext.SaveChangesAsync();

            }

          

        }
    }
}
