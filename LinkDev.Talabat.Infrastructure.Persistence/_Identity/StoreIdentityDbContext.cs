using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity
{
	public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			//builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);

			builder.ApplyConfiguration(new ApplicationUserConfigurations());
			builder.ApplyConfiguration(new AddressConfigurations());
		}

	}
}
