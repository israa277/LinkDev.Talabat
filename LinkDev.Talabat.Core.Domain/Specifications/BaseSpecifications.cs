﻿using LinkDev.Talabat.Core.Domain.Contracts;
using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity,bool>>? Criteria { get; set; } = null;
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new ();

		public BaseSpecifications()
		{

		}
		public BaseSpecifications(TKey id)
		{
			Criteria = E => E.Id.Equals(id);
		}

	}
}
