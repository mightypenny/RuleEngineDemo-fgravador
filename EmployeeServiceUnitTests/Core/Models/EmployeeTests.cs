using EmployeeService.Core.Models;

namespace EmployeeServiceUnitTests.Core.Models;

public class EmployeeTests
{
    [Fact]
    public void Should_Calculate_AnnualSalary()
    {
        // given
        var employee = new Employee
        {
            Salary = 100
        };

        // when
        var annualSalary = employee.AnnualSalary();
        
        // then
        Assert.Equal(1200, annualSalary);
    }
    
    [Fact]
    public void Should_Calculate_Bonus()
    {
        // given
        var employee = new Employee
        {
            Salary = 1000
        };

        // when
        var bonus = employee.Bonus();
        
        // then
        Assert.Equal(3600, bonus);
    }
    
    [Fact]
    public void Should_Calculate_Age()
    {
        // given
        var employee = new Employee
        {
            BirthDate = DateTime.Now.AddYears(-12)
        };

        // when
        var age = employee.Age();
        
        // then
        Assert.Equal(12, age);
    }
}