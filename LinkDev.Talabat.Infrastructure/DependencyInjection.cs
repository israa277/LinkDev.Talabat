﻿using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Infrastructure.Basket_Repository;
using LinkDev.Talabat.Infrastructure.Payment_Service;
using LinkDev.Talabat.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LinkDev.Talabat.Infrastructure
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services ,IConfiguration configuration)
		{
			services.AddScoped(typeof(IConnectionMultiplexer), (serviceProvider) =>
			{
				var connectionString = configuration.GetConnectionString("Redis");
				var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
				return connectionMultiplexerObj;
			});
			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			services.Configure<RadisSettings>(configuration.GetSection("RedisSettings"));
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
			return services;
		}


	}
}
