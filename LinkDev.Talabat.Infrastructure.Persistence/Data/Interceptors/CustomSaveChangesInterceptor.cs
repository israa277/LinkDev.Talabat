﻿using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
	internal class CustomSaveChangesInterceptor : SaveChangesInterceptor 
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public CustomSaveChangesInterceptor(ILoggedInUserService loggedInUserService)
		{
			_loggedInUserService = loggedInUserService;
		}
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntities(eventData.Context);
				
			return base.SavingChanges(eventData, result);
		}
		public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);

			return base.SavedChangesAsync(eventData, result, cancellationToken);
		}

		private void UpdateEntities(DbContext? dbContext)
		{
			if (dbContext is null)
				return;
			foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>()
				.Where(entity => entity.State is EntityState.Added or EntityState.Modified))
			{
				
				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedBy = _loggedInUserService.UserId!;
					entry.Entity.CreatedOn = DateTime.UtcNow;
				}
				entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entry.Entity.LastModifiedOn = DateTime.UtcNow;

			}
		}

	}
}
