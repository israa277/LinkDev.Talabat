using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Shared.Basket;

namespace LinkDev.Talabat.Core.Domain.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId);
    }
}
