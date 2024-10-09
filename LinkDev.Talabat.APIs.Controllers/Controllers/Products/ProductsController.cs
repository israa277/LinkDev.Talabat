﻿using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var products = await serviceManager.ProductService.GetProductsAsync();
			return Ok(products);
		}

	}
}
