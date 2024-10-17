namespace LinkDev.Talabat.Core.Domain.Entities.Basket
{
	public class CustomerBasket
	{
		public required string  Id { get; set; }
		public IEnumerable<BasketItem> Items { get; set; } = new List<BasketItem>();

	}
}
