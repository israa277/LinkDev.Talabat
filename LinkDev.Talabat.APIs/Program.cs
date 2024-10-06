using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			//StoreContext dbContext = new StoreContext();
			var webApplicationbuilder = WebApplication.CreateBuilder(args);

			#region Configure Services
			// Add services to the container.

			webApplicationbuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationbuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
			webApplicationbuilder.Services.AddPersistenceServices(webApplicationbuilder.Configuration);
			#endregion

			#region Update Database 
			var app = webApplicationbuilder.Build();

			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;
			var dbContext = services.GetRequiredService<StoreContext>();

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				var pendingMigrations = dbContext.Database.GetPendingMigrations();
				if (pendingMigrations.Any())
					await dbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsun(dbContext);

			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occured during applying the migrations or the data seeding");
			} 
			#endregion

			#region Configure Kestrel Middlewares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
