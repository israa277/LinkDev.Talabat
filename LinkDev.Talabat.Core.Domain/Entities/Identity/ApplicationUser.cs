﻿using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Core.Domain.Entities.Identity
{
	public class ApplicationUser : IdentityUser<string>
	{
		public required string DisplayName { get; set; }
		public Address? Address { get; set; }
	}
}