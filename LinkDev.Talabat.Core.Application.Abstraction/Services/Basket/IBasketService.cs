using LinkDev.Talabat.Shared.Basket;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Basket
{
    public interface IBasketService
	{
		Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId);
		Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasket);
		Task DeleteCustomerBasketAsync(string basketId);



	}
}
