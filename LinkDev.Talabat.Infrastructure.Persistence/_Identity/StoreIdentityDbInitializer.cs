using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
	internal sealed class StoreIdentityDbInitializer(StoreIdentityDbContext _dbContext,UserManager<ApplicationUser> _userManager) : DbInitializer(_dbContext),IStoreIdentityDbInitializer
	{	

		public override async Task SeedAsync()
		{
			var user = new ApplicationUser()
			{
				DisplayName = "Israa Ahmed",
				UserName = "Israa.Ahmed",
				Email = "israa.ahmed277@gmail.com",
				PhoneNumber = "01144972912",
				
			};
			await _userManager.CreateAsync(user,"P@ssword");
		}
	}
}
