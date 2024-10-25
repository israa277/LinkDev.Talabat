﻿using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto model);
		Task<UserDto> RegisterAsync(RegisterDto model);

	}
}