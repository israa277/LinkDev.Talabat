using LinkDev.Talabat.Core.Domain.Contracts.Persistence;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
	{
		public static async Task<WebApplication> InitializerStoreContextAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;
			var storeContextIntitializer = services.GetRequiredService<IStoreContextInitializer>();

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await storeContextIntitializer.InitializeAsync();
				await storeContextIntitializer.SeedAsync();

			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occured during applying the migrations or the data seeding");
			}
			return app;
		}
	}
}
