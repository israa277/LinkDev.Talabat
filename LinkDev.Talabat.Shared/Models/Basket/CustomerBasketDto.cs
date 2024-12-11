using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Shared.Basket
{
	public class CustomerBasketDto
	{
        [Required]
		public required string Id { get; set; }
		public  List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        
    }
}
