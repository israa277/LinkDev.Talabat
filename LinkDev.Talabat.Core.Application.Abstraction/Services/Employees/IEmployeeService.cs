using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Employees
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeToReturnDto>> GetEmployeeAsync();

		Task<EmployeeToReturnDto> GetEmployeeAsync(int id);
	}
}
