using System.Formats.Asn1;
using AutoMapper;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications.Orders;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
    internal class OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketService basketService , IPaymentService paymentService) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // 1.Get Basket From Baskets Reop

            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);
            // 2. Get Selected Items at Basket From Products Reop

            var orderItems = new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product is not null)
                    {
                        var productItemOrdered = new ProductItemOrdered()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl ?? "",
                        };
                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity
                        };
                        orderItems.Add(orderItem);
                    }
                }
            }
            // 3. Calculate SubTotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            // 4. Map Address

            var address = mapper.Map<Address>(order.ShippingAddress);
            //5.Get Delivery Method
            var delivaryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);
            // 6. Create Order
            var orderRepo = unitOfWork.GetRepository<Order, int>();

            var orderSpecs = new OrderByPaymentIntentSpecifications(basket.PaymentIntentId!);
            
            var existingOrder = await orderRepo.GetWithSpecAsync(orderSpecs);
            if (existingOrder is not null)
            {
                orderRepo.Delete(existingOrder);
                await paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }

            var orderToCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                DeliveryMethod = delivaryMethod,
                Items = orderItems,
                Subtotal = subTotal,
                PaymentIntentId = basket.PaymentIntentId!
            };
            await orderRepo.AddAsync(orderToCreate);
            // 7.Save To DataBase
            var created = await unitOfWork.CompleteAsync() > 0;
            if (!created) throw new BadRequestException("AN Error has occured during creating the order");

            return mapper.Map<OrderToReturnDto>(orderToCreate);
        }
        public async Task<IEnumerable<OrderToReturnDto>> GetOrderForUserAsync(string buyerEmail)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail);
            var orders = await unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(orderSpecs);
            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {

            var orderSpecs = new OrderSpecifications(buyerEmail, orderId);
            var order = await unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(orderSpecs);
            if (order is null) throw new NotFoundException(nameof(Order), orderId);
            return mapper.Map<OrderToReturnDto>(order);
        }
        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }




    }
}
