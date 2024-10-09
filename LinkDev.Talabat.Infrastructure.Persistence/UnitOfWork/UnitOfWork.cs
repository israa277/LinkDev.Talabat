using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private readonly ConcurrentDictionary<string, object> _repositories;
		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new();
		}
		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseAuditableEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity,TKey>(_dbContext));
		}

		public async Task<int> Completesync() => await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();


	}
}
