namespace EmployeeService.Core.Models;

public class Employee
{
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; } 
    
    public int Age()
    {
        return DateTime.Now.Subtract(BirthDate).Days / 365;
    }
    
    public double Salary { get; set; }

    public double Bonus()
    {
        return AnnualSalary() * 0.3;
    }
    
    public double AnnualSalary()
    {
        return Salary * 12;
    }
}
