﻿using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
	internal abstract class DbInitializer(DbContext _dbContext) : IDbInitializer
	{
		public virtual async Task InitializeAsync()
		{
			var pendingMigrations = await _dbContext.Database.GetAppliedMigrationsAsync();
			if (pendingMigrations.Any())
				await _dbContext.Database.MigrateAsync();

		}

		public abstract Task SeedAsync();
	
	}
}
