﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Specifications.Employees;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
	internal class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
	{
		public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeeAsync()
		{
			var spec = new EmployeeWithDepartmentSpecifications();
			var employees = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);
		    var employeeToReturns = mapper.Map<IEnumerable<EmployeeToReturnDto>>(employees);
			return employeeToReturns;


		}

		public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
		{
			var spec = new EmployeeWithDepartmentSpecifications(id);
			var employees = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);
			var employeeToReturns = mapper.Map<EmployeeToReturnDto>(employees);
			return employeeToReturns;

		}
	}
}

