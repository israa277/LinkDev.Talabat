﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications.Products;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
	internal class ProductServices(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
	{

		public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(specParams.Sort,specParams.BrandId,specParams.CategoryId,specParams.PageSize,specParams.PageIndex,specParams.Search);
			var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
			var data = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
			var countSpec = new ProductWithFilterationForCountSpecifications(specParams.BrandId, specParams.CategoryId,specParams.Search);
			var count = await unitOfWork.GetRepository<Product,int>().GetCountAsyn(countSpec) ;
			
			return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize , count) { Data = data};
		}

		public async Task<ProductToReturnDto> GetProductsAsync(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var product = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
			
			if(product is null)
				throw new NotFoundException(nameof(Product),id);
			
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
