using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Applicarion;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
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
					.Select(P => new ApiValidationErrorResponse.ValidationError()
					{
						Field = P.Key,
						Errors = P.Value!.Errors.Select(E=>E.ErrorMessage)
					});

						return new BadRequestObjectResult(new ApiValidationErrorResponse()
						{
							Errors = errors
						});

					};
				})
				.AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);

	
			
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationbuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

			webApplicationbuilder.Services.AddHttpContextAccessor();

			webApplicationbuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
			webApplicationbuilder.Services.AddApplicationServices();
			webApplicationbuilder.Services.AddPersistenceServices(webApplicationbuilder.Configuration);
			webApplicationbuilder.Services.AddInfrastructureServices(webApplicationbuilder.Configuration);

			//webApplicationbuilder.Services.AddIdentity<ApplicationUser, IdentityRole>();
			webApplicationbuilder.Services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
			{
				identityOptions.SignIn.RequireConfirmedAccount = true;
				identityOptions.SignIn.RequireConfirmedEmail = true;
				identityOptions.SignIn.RequireConfirmedPhoneNumber = true;	
				
				
				//identityOptions.Password.RequireNonAlphanumeric = true;
				//identityOptions.Password.RequiredUniqueChars = 2;
				//identityOptions.Password.RequiredLength = 6;
				//identityOptions.Password.RequireDigit = true;
				//identityOptions.Password.RequireUppercase = true;

				identityOptions.User.RequireUniqueEmail = true;
				//identityOptions.User.AllowedUserNameCharacters = "abcdenkotlog93124568_-+@#$"
			
				identityOptions.Lockout.AllowedForNewUsers = true;
				identityOptions.Lockout.MaxFailedAccessAttempts = 5;
				identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(12);

				//identityOptions.Stores;
				//identityOptions.Tokens
				//identityOptions.ClaimsIdentity
			
			}).AddEntityFrameworkStores<StoreIdentityDbContext>();
		
			
			#endregion

			var app = webApplicationbuilder.Build();

			#region Databases Initialization
			await app.InitializerDbAsync();

			#endregion

			#region Configure Kestrel Middlewares
			// Configure the HTTP request pipeline.

			app.UseMiddleware<ExceptionHandlerMiddelware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseStatusCodePagesWithReExecute("/Errors/{0}");

			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
