﻿using System.Security.Claims;
using LinkDev.Talabat.Core.Application.Abstraction.Models._Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto loginDto);
		Task<UserDto> RegisterAsync(RegisterDto registerDto);
		Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
		Task<AddressDto?> GetUserAddress (ClaimsPrincipal claimsPrincipal);
        Task<AddressDto> UpdateUserAddress (ClaimsPrincipal claimsPrincipal,AddressDto addressDto);

    }
}
