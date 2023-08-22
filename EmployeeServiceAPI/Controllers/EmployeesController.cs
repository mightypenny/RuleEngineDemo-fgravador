using AutoMapper;
using EmployeeService.Core.Models;
using EmployeeService.Core.Services;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService employeeService;
    private readonly IMapper mapper;
    private readonly ILogger<EmployeesController> logger;

    public EmployeesController(
        IEmployeeService employeeService,
        IMapper mapper,
        ILogger<EmployeesController> logger)
    {
        this.employeeService = employeeService;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpGet(Name = "GetEmployees")]
    public async Task<IEnumerable<EmployeeExtDto>> Get()
    {
        var employees = await employeeService
            .GetEmployees()
            .ConfigureAwait(false);
        
        var employeesDto = employees
            .Select(employee => mapper.Map<EmployeeExtDto>(employee))
            .ToList();
        
        logger.LogInformation("{Count} Employee records found", employeesDto.Count);

        return employeesDto;
    }
    
    [HttpGet("{id}", Name = "GetEmployeeById")]
    public async Task<EmployeeExtDto> GetById(string id)
    {
        var employee = await employeeService
            .GetEmployeeById(id)
            .ConfigureAwait(false);
        
        logger.LogInformation("Employee {Id} data found", id);
        
        return mapper.Map<EmployeeExtDto>(employee);
    }
    
    [HttpPost(Name = "CreateEmployee")]
    public async Task<string> CreateEmployee(EmployeeDto employeeDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);
        
        var id = await employeeService
            .CreateEmployee(employee)
            .ConfigureAwait(false);
        
        logger.LogInformation("Successfully saved employee {Id}", id);
        
        return id;
    }
}
