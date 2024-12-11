using System.Text;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

			//webApplicationbuilder.Services.AddIdentity<ApplicationUser, IdentityRole>();
			services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
			{
				//identityOptions.SignIn.RequireConfirmedAccount = true;
				//identityOptions.SignIn.RequireConfirmedEmail = true;
				//identityOptions.SignIn.RequireConfirmedPhoneNumber = true;


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


			services.AddAuthentication((authenctionOptions) =>
			{
				authenctionOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				authenctionOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer((configurationOptions) =>
				{
					configurationOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,

						ValidAudience = configuration["JWTSettings:Audience"],
						ValidIssuer = configuration["JWTSettings:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!)),
						ClockSkew = TimeSpan.Zero


					};
				});
				


			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
			{

				return () => serviceProvider.GetRequiredService<IAuthService>();
			});

			return services;
		}
	}
}
