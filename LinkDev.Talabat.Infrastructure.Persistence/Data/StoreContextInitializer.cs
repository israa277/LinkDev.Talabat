using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	internal class StoreContextInitializer(StoreContext _dbContext) : IStoreContextInitializer
	{
	
		public async Task InitializeAsync()
		{
			var pendingMigrations = await _dbContext.Database.GetAppliedMigrationsAsync();
			if (pendingMigrations.Any())
				await _dbContext.Database.MigrateAsync();
			
		}

		public async Task SeedAsync()
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
