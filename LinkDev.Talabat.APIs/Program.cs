using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;

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

			webApplicationbuilder.Services.AddControllers().AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationbuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

			webApplicationbuilder.Services.AddHttpContextAccessor();

			webApplicationbuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(ILoggedInUserService));

			webApplicationbuilder.Services.AddPersistenceServices(webApplicationbuilder.Configuration);


			#endregion

			var app = webApplicationbuilder.Build();

			#region Databases Initialization
			await app.InitializerStoreContextAsync();

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
