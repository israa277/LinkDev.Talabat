﻿using LinkDev.Talabat.Core.Applicarion.Exceptions;
using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Core.Applicarion.Services.Auth
{
	internal class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
	{
		public async Task<UserDto> LoginAsync(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user is null) throw new BadRequestException("Invalid Login");
			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
			if (!result.Succeeded) throw new BadRequestException("Invalid Login");

			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = "This will be JWT Token"
			};

			return response;
		}

		public async Task<UserDto> RegisterAsync(RegisterDto model)
		{
			var user = new ApplicationUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.UserName,
				PhoneNumber = model.Phone
			};
			var result = await userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded) throw new ValidationException() { Errors = result.Errors.Select(E=>E.Description)};
			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = "This will be JWT Token"
			};

			return response;

		}
	}
}
