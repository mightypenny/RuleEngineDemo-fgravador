namespace EmployeeService.Repositories.Postgres;

public class Employee
{
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public double Salary { get; set; }
}