using System.Text.Json.Serialization;

namespace EmployeeService.Models;

public class EmployeeExtDto : EmployeeDto
{
    [JsonPropertyOrder(1)]
    public string FullName { get; set; }

    [JsonPropertyOrder(2)]
    public int Age { get; set; }
    
    [JsonPropertyOrder(3)]
    public double BonusDifferentName { get; set; }
    
    [JsonPropertyOrder(4)]
    public double AnnualSalary { get; set; }
}
