using LinkDev.Talabat.Core.Applicarion.Mapping;
using LinkDev.Talabat.Core.Applicarion.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Applicarion
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)

		{
			services.AddAutoMapper(typeof(MappingProfile));
			//services.AddScoped(typeof(IProductService), typeof(IProductService));
			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
			return services;

		}
	}
}
