using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);
        Task<IEnumerable<OrderToReturnDto>> GetOrderForUserAsync(string buyerEmail);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();

    }
}
