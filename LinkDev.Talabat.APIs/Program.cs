using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Applicarion;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

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

			webApplicationbuilder.Services.AddControllers()
				.ConfigureApiBehaviorOptions(options =>
				{
					options.SuppressModelStateInvalidFilter = false;
					options.InvalidModelStateResponseFactory = (actionContext) =>
					{
						var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
					.SelectMany(P => P.Value!.Errors)
					.Select(E => E.ErrorMessage);

						return new BadRequestObjectResult(new ApiValidationErrorResponse()
						{
							Errors = errors
						});

					};
				})
				.AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);

			webApplicationbuilder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = false;
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
				.SelectMany(P => P.Value!.Errors)
				.Select(E => E.ErrorMessage);

					return new BadRequestObjectResult(new ApiValidationErrorResponse()
					{
						Errors = errors
					});

				};
			});
			
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationbuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

			webApplicationbuilder.Services.AddHttpContextAccessor();

			webApplicationbuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

			webApplicationbuilder.Services.AddPersistenceServices(webApplicationbuilder.Configuration);
			webApplicationbuilder.Services.AddApplicationServices();

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
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
