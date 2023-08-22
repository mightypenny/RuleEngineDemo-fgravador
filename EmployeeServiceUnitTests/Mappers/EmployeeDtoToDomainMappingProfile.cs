using AutoMapper;
using EmployeeService.Core.Models;
using EmployeeService.Mappers;
using EmployeeService.Models;

namespace EmployeeServiceUnitTests.Mappers;

public class EmployeeDtoToDomainMappingProfileUnitTests
{
    private readonly IMapper mapper;

    public EmployeeDtoToDomainMappingProfileUnitTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new EmployeeMappingProfile());
        });

        mapper = new Mapper(mapperConfig);

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Test()
    {
        var dto = new EmployeeDto
        {
            FirstName = "francis",
            LastName = "gravador"
        };
        
        var result = mapper.Map<Employee>(dto);
        
        Assert.NotNull(result);
        Assert.Equal(dto.FirstName, result.FirstName);
        Assert.Equal(dto.LastName, result.LastName);
    }
}