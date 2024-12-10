using LinkDev.Talabat.Core.Domain.Entities.Basket;

namespace LinkDev.Talabat.Core.Domain.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
