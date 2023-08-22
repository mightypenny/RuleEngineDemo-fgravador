using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeService.Repositories.MongoDB;

public class Employee
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public double Salary { get; set; }
}