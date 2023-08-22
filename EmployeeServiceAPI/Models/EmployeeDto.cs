using System.Text.Json.Serialization;

namespace EmployeeService.Models;

public class EmployeeDto
{
    [JsonPropertyOrder(1)]
    public string Id { get; set; }
    
    [JsonPropertyOrder(2)]
    public string FirstName { get; set; }
    
    [JsonPropertyOrder(3)]
    public string LastName { get; set; }

    [JsonPropertyOrder(4)]
    public DateTime BirthDate { get; set; } 
    
    [JsonPropertyOrder(5)]
    public double Salary { get; set; }
}
