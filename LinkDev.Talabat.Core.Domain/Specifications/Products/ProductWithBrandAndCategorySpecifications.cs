using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		public ProductWithBrandAndCategorySpecifications() : base()
		{
			AddIncludes();
		}



		public ProductWithBrandAndCategorySpecifications(int id) : base(id)
		{
			AddIncludes();
		}
		private void AddIncludes()
		{
			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}
	}
}
