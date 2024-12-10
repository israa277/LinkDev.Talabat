using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Shared.Models;
using Microsoft.Extensions.Options;
using Stripe;
using Product = LinkDev.Talabat.Core.Domain.Entities.Products.Product;

namespace LinkDev.Talabat.Infrastructure.PaymentService
{
    internal class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork,IOptions<RadisSettings> radisSettings) : IPaymentService
    {
        private readonly RadisSettings _radisSettings = radisSettings.Value;
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await basketRepository.GetAsync(basketId);
            if (basket is null) throw new NotFoundException(nameof(CustomerBasket), basketId);
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                if(deliveryMethod is null) throw new NotFoundException(nameof(DeliveryMethod), basket.DeliveryMethodId.Value);

                basket.ShippingPrice = deliveryMethod.Cost;
            }
            if (basket.Items.Count > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product is null) throw new NotFoundException(nameof(Product), item.Id);

                    if (item.Price != product!.Price)
                        item.Price = product.Price;
                }
            }
            PaymentIntent? paymentIntent = null;
            PaymentIntentService paymentIntwntService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "Card" }
                };
                paymentIntent = await paymentIntwntService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }

            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,

                };
                await paymentIntwntService.UpdateAsync(basket.PaymentIntentId, options);
            }
            await basketRepository.UpdateAsync(basket,TimeSpan.FromDays(_radisSettings.TimeToLiveInDays));
            return basket;
        }
    }
}
