using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
    internal class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public AuditInterceptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null)
                return;



            var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
                .Where(entry => entry.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in entries)
            {
                //var entity = entry.Entity;
                //var userId = _loggedInUserService.UserId;
                //var cureentTime = DateTime.UtcNow;
                //if(entry.Entity is Order or OrderItem)
                //{
                //    _loggedInUserService.UserId = "";
                //}
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = _loggedInUserService.UserId!;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    //SetPropertyIfExists(entity, "CreatedBy", userId!);
                    //SetPropertyIfExists(entity, "CreatedOn", cureentTime);
                }
                entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
                //SetPropertyIfExists(entity, "LastModifiedBy", userId!);
                //SetPropertyIfExists(entity, "LastModifiedOn", cureentTime);
            }
        }

        //private void SetPropertyIfExists(object entity, string propertyName, object value)
        //{
        //    var property = entity.GetType().GetProperty(propertyName);
        //    if (property != null && property.CanWrite)
        //    {
        //        property.SetValue(entity, value);
        //    }
        //}

    }
}
