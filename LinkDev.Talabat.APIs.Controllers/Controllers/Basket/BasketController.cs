﻿using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Basket
{
	public class BasketController(IServiceManager serviceManager) : BaseApiController
	{
		[HttpGet] //Get: /api/Basket?id=
		public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
		{
			var basket = await serviceManager.BasketService.GetCustomerBasketAsync(id);
			return Ok(basket);
		}

		[HttpPost] //Post: /api/Basket
		public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto )
		{
			var basket = await serviceManager.BasketService.UpdateCustomerBasketAsync(basketDto);
			return Ok(basket);
		}

		[HttpDelete] //DELETE: /api/Basket
		public async Task DeleteBasket(string id)
		{
			 await serviceManager.BasketService.DeleteCustomerBasketAsync(id);
		
		}


	}
}