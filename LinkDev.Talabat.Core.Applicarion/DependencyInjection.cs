using LinkDev.Talabat.Core.Applicarion.Mapping;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Applicarion
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)

		{
			services.AddAutoMapper(typeof(MappingProfile));
			services.AddScoped(typeof(IProductService), typeof(IProductService));
			return services;

		}
	}
}
