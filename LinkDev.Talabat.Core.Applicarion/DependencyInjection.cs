using LinkDev.Talabat.Core.Applicarion.Mapping;
using LinkDev.Talabat.Core.Applicarion.Services;
using LinkDev.Talabat.Core.Applicarion.Services.Basket;
using LinkDev.Talabat.Core.Applicarion.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Applicarion
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)

        {
            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddScoped(typeof(IProductService), typeof(IProductService));

            //services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            //{
            //	var mapper = serviceProvider.GetRequiredService<IMapper>();
            //	var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            //	var basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();
            //	return () => new BasketService(basketRepository , mapper , configuration);
            //});
            services.AddScoped(typeof(IBasketService), typeof(BasketService));

            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IBasketService>();
            });

            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped(typeof(Func<IOrderService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IOrderService>();
            });

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));


            return services;

        }
    }
}
