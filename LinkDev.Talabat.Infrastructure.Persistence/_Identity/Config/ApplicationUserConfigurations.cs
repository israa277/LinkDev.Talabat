﻿using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity.Config
{
	internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(U => U.DisplayName)
				.HasColumnType("varchar")
				.HasMaxLength(100)
				.IsRequired(true);
			builder.HasOne(U => U.Address)
				.WithOne(A => A.User)
				.HasForeignKey<Address>(A => A.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
