using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>()
			where TEntity: BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>;

		Task<int> Completesync();
	}
}
