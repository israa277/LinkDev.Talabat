using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
	internal static class SpecificationsEvaluator<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity, TKey> spec)
		{
			var query = inputQuery;

			if(spec.Criteria is not null)
				query = query.Where(spec.Criteria);
			if(spec.OrderByDesc is not null) 
				query =query.OrderByDescending(spec.OrderByDesc);
			else if(spec.OrderBy is not null)
				query=query.OrderBy(spec.OrderBy);

			if(spec.IsPaginationEnabled)
				query =query.Skip(spec.Skip).Take(spec.Take);


			query = spec.Includes.Aggregate(query,(currentQuery,includeExpression) => currentQuery.Include(includeExpression));

			return query;
		}
	}
}
