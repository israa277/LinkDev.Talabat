using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsun(StoreContext dbContext)
		{
			if(!dbContext.Brands.Any())
			{
				var brandData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
				if (brands?.Count() > 0)
				{
					await dbContext.Set<ProductBrand>().AddRangeAsync(brands);
					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.Categories.Any())
			{
				var categoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
				if (categories?.Count() > 0)
				{
					await dbContext.Set<ProductCategory>().AddRangeAsync(categories);
					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.Categories.Any())
			{
				var productData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/product.json");
				var product = JsonSerializer.Deserialize<List<Product>>(productData);
				if (product?.Count() > 0)
				{
					await dbContext.Set<Product>().AddRangeAsync(product);
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
