using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications.Products;

namespace LinkDev.Talabat.Core.Applicarion.Services.Products
{
	internal class ProductServices(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
	{

		public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
		

			var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
			var productsToReturn = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
			return productsToReturn;
		}

		public async Task<ProductToReturnDto> GetProductsAsync(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var product = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
			var productsToReturn = mapper.Map<ProductToReturnDto>(product);
			return productsToReturn;
		}
		public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
		{
			var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			var brandsToReturn = mapper.Map<IEnumerable<BrandDto>>(brands);
			return brandsToReturn;
		}

		public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
		{
			var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
			var categoriesToReturn = mapper.Map<IEnumerable<CategoryDto>>(categories);
			return categoriesToReturn;
		}


	}
}
