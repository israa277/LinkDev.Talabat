using System.Security.Claims;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Core.Application.Extensions
{
    internal static class UserManagerExtension
    {
        public static async Task<ApplicationUser?> FindUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Where(user => user.Email == email).Include(user => user.Address).FirstOrDefaultAsync();
            return user;
        }
    }
}
