using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		public ProductWithBrandAndCategorySpecifications(string? sort) : base()
		{
			AddIncludes();
			AddOrderBy(P=>P.Name);
			if (!string.IsNullOrWhiteSpace(sort))
			{
				switch (sort)
				{
					case "nameDesc":
						AddOrderByDesc(P => P.Name);
						break;
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;
					default:
						AddOrderBy(P=>P.Name);
						break;
				}
			}

		}



		public ProductWithBrandAndCategorySpecifications(int id) : base(id)
		{
			AddIncludes();
		}

		private protected override void AddIncludes()
		{
			base.AddIncludes();
			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}
	}
}
