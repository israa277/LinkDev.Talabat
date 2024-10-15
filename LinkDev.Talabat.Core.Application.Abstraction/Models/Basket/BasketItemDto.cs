using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Basket
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage ="Product Name is Required!")]
		public required string ProductName { get; set; }
		public string? PictureUrl { get; set; }
		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Price must be greather than zero")]
		public decimal Price { get; set; }
		[Required]
		[Range(.1, int.MaxValue, ErrorMessage = "Quantity must be at least one item")]

		public int Quantity { get; set; }
		public string? Brand { get; set; }
		public string? Category { get; set; }
	}

}
