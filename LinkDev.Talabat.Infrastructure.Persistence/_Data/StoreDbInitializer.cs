using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	internal sealed class StoreDbInitializer(StoreDbContext _dbContext) : DbInitializer(_dbContext) ,IStoreDbInitializer
	{
	

		public override async Task SeedAsync()
		{
			if (!_dbContext.Brands.Any())
			{
				var brandData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
				if (brands?.Count() > 0)
				{
					await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
					await _dbContext.SaveChangesAsync();
				}
			}

			if (!_dbContext.Categories.Any())
			{
				var categoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
				if (categories?.Count() > 0)
				{
					await _dbContext.Set<ProductCategory>().AddRangeAsync(categories);
					await _dbContext.SaveChangesAsync();
				}
			}

			if (!_dbContext.Categories.Any())
			{
				var productData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/product.json");
				var product = JsonSerializer.Deserialize<List<Product>>(productData);
				if (product?.Count() > 0)
				{
					await _dbContext.Set<Product>().AddRangeAsync(product);
					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
