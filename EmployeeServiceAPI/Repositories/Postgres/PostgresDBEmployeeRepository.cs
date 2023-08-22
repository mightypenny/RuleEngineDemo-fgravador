using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories.Postgres;

public class PostgresDBEmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeDBContext employeeDbContext;
    private readonly IMapper mapper;

    public PostgresDBEmployeeRepository(
        EmployeeDBContext employeeDbContext,
        IMapper mapper)
    {
        this.employeeDbContext = employeeDbContext;
        this.mapper = mapper;
    }
    
    public async Task<IEnumerable<Core.Models.Employee>> GetEmployees()
    {
        var employees = await employeeDbContext
            .Employees
            .ToListAsync()
            .ConfigureAwait(false);
        
        return employees
            .Select(employee => mapper.Map<Core.Models.Employee>(employee))
            .ToList();
    }

    public async Task<Core.Models.Employee?> GetEmployeeById(string id)
    {
        var employee = await employeeDbContext
            .Employees
            .FirstOrDefaultAsync(r => r.Id.Equals(id))
            .ConfigureAwait(false);
        
        return employee != null 
            ? mapper.Map<Core.Models.Employee>(employee) 
            : null;
    }

    public async Task<string> CreateEmployee(Core.Models.Employee employee)
    {
        var employeeDBType = mapper.Map<Employee>(employee);
        
        employeeDbContext.Employees.Add(employeeDBType); 
        await employeeDbContext
            .SaveChangesAsync()
            .ConfigureAwait(false);

        return employeeDBType.Id;
    }
}