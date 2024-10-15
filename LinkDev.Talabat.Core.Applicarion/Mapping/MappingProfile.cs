using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Applicarion.Mapping
{
	internal class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.Brand, O => O.MapFrom(src => src.Brand!.Name))
				.ForMember(d => d.Category, O => O.MapFrom(src => src.Category!.Name))
				//.ForMember(d => d.PictureUrl, O => O.MapFrom(s=> $"{"https://localhost:7034"}{s.PictureUrl}"));
				.ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());


			CreateMap<ProductBrand, BrandDto>();
			CreateMap<ProductCategory, CategoryDto>();
			CreateMap<Employee, EmployeeToReturnDto>();
			CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
			CreateMap<BasketItem,BasketItemDto>().ReverseMap();

		}
	}
}
