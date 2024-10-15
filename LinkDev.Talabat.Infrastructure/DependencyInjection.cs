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
			return services;
		}


	}
}
