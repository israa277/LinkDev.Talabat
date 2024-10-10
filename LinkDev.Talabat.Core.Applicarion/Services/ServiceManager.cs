﻿using AutoMapper;
using LinkDev.Talabat.Core.Applicarion.Services.Employees;
using LinkDev.Talabat.Core.Applicarion.Services.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;

namespace LinkDev.Talabat.Core.Applicarion.Services
{
	internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<EmployeeService> _employeeService;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_productService = new Lazy<IProductService>(() => new ProductServices(_unitOfWork,_mapper));
			_employeeService = new Lazy<EmployeeService>(() => new EmployeeService(_unitOfWork,_mapper));


		}
		public IProductService ProductService => _productService.Value;
		public IEmployeeService EmployeeService => _employeeService.Value;
	}
}
