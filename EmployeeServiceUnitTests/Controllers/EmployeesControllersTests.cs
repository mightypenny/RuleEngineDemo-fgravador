using AutoMapper;
using EmployeeService.Controllers;
using EmployeeService.Core.Models;
using EmployeeService.Core.Services;
using EmployeeService.Mappers;
using EmployeeService.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeeServiceUnitTests.Controllers;

public class EmployeesControllersTests
{
    private readonly Mock<IEmployeeService> employeeServiceMock = new();
    private readonly IMapper mapper;
    private readonly Mock<ILogger<EmployeesController>> loggerMock = new();
    
    private readonly EmployeesController controller;

    public EmployeesControllersTests()
    {
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new EmployeeMappingProfile()); });
        mapper = mappingConfig.CreateMapper();

        controller = new EmployeesController(
            employeeServiceMock.Object,
            mapper,
            loggerMock.Object);
    }

    [Fact]
    public void Test()
    {
        // given
        employeeServiceMock
            .Setup(x => x.GetEmployees())
            .ReturnsAsync(() => new[] { new Employee { LastName = "Gravador"} });
        
        // when
        var result = controller.Get().Result;
        
        // then
        Assert.NotNull(result);
        Assert.Equal("Gravador", result.ToList()[0].LastName);
    }
}