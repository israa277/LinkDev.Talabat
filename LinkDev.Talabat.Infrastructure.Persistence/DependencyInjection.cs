﻿using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<StoreContext>((optionsBuilder) =>
			{
				optionsBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
			}/*,contextLifetime: ServiceLifetime.Scoped,optionsLifetime:ServiceLifetime.Scoped*/);
			return services;
		}
	}
}
