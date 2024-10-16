﻿using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{
			var products = await serviceManager.ProductService.GetProductsAsync(specParams);
			return Ok(products);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var product = await serviceManager.ProductService.GetProductsAsync(id);	
			//if(product == null)
			//{
			//	return NotFound(new ApiResponse(404 , $"the Product with id:{id} is not found"));
			//}


			return Ok(product);
		}


		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
		{
			var brands = await serviceManager.ProductService.GetBrandsAsync();
		
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
		{
			var categories = await serviceManager.ProductService.GetCategoriesAsync();

			return Ok(categories);
		}

	}
}
