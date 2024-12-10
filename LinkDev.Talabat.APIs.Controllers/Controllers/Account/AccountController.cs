using System.Security.Claims;
using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models._Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
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

		[Authorize]
		[HttpGet("getcurrentuser")] //GET : /api/account/getcurrentuser

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await serviceManager.AuthService.GetCurrentUser(User);
            return Ok(result);
        }


        [Authorize]
        [HttpGet("address")] //GET : /api/account/address

        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var result = await serviceManager.AuthService.GetUserAddress(User);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("address")] //GET : /api/account/address

        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var result = await serviceManager.AuthService.UpdateUserAddress(User , address);
            return Ok(result);
        }

      
        [HttpGet("emailexists")] //GET : /api/account/emailexists?email=israa.ahmed277@gmail.com

        public async Task<ActionResult<AddressDto>> CheckEmailExists(string email)
        {
            return Ok(await serviceManager.AuthService.EmailExists(email!));
        }
    }
}
