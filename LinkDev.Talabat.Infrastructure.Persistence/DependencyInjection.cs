using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			#region Store Context
			services.AddDbContext<StoreDbContext>((serviceProvider,optionsBuilder) =>
				{
					optionsBuilder
					.UseLazyLoadingProxies()
					.UseSqlServer(configuration.GetConnectionString("StoreContext"))
					.AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
				
				}/*,contextLifetime: ServiceLifetime.Scoped,optionsLifetime:ServiceLifetime.Scoped*/);

			//services.AddScoped<IStoreDbInitializer, StoreDbInitializer>();
			services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));
            services.AddScoped(typeof(AuditInterceptor));
			
			//services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
			#endregion

			#region Identity Context
			services.AddDbContext<StoreIdentityDbContext>((optionsBuilder) =>
			{
				optionsBuilder
				.UseLazyLoadingProxies()
				.UseSqlServer(configuration.GetConnectionString("IdentityContext"));
			}/*,contextLifetime: ServiceLifetime.Scoped,optionsLifetime:ServiceLifetime.Scoped*/);
			services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));

			#endregion

			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

			return services;


		}
	}
}
