using EmployeeService.Core.Models;

namespace EmployeeService.Core.Services;

public interface IEmployeeService
{
    public Task<IEnumerable<Employee>> GetEmployees();
    
    public Task<Employee> GetEmployeeById(string id);

    public Task<string> CreateEmployee(Employee employee);
}