using AutoMapper;
using LinkDev.Talabat.Core.Applicarion.Services.Employees;
using LinkDev.Talabat.Core.Applicarion.Services.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Applicarion.Services
{
	internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<EmployeeService> _employeeService;
		private readonly Lazy<IBasketService> _basketService;
		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper , IConfiguration configuration , Func<IBasketService> basketServiceFactory)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_configuration = configuration;
			_productService = new Lazy<IProductService>(() => new ProductServices(_unitOfWork,_mapper));
			_employeeService = new Lazy<EmployeeService>(() => new EmployeeService(_unitOfWork,_mapper));
			_basketService = new Lazy<IBasketService>(basketServiceFactory);

		}
		public IProductService ProductService => _productService.Value;
		public IEmployeeService EmployeeService => _employeeService.Value;

		public IBasketService BasketService =>_basketService.Value;
	}
}
