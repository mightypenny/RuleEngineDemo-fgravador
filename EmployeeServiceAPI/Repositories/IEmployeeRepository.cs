using EmployeeService.Core.Models;

namespace EmployeeService.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployees();

    Task<Employee?> GetEmployeeById(string id);

    Task<string> CreateEmployee(Employee employee);
}