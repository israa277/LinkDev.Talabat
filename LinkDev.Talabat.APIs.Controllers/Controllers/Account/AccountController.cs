using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
	public class AccountController(IServiceManager serviceManager) : BaseApiController
	{
		[HttpPost("login")] //Post : /api/account/login
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var response = await serviceManager.AuthService.LoginAsync(model);
			return Ok(response);
		}


		[HttpPost("register")] //Post : /api/account/register
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var response = await serviceManager.AuthService.RegisterAsync(model);
			return Ok(response);
		}
	}
}
