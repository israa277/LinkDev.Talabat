﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
	internal class GenericRepository<TEntity, TKey>(StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
	
		public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
		=> withTracking ? await _dbContext.Set<TEntity>().ToListAsync() : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		public async Task<TEntity?> GetAsync(TKey id)
		=> await _dbContext.Set<TEntity>().FindAsync(id);
		public async Task AddAsync(TEntity entity)
		=> await _dbContext.Set<TEntity>().AddAsync(entity);

		public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
		public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

	}
}
