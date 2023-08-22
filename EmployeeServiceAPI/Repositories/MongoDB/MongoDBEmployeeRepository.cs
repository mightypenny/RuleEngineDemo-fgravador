using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeeService.Repositories.MongoDB;

public class MongoDBEmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> collection;
    private readonly IMapper mapper;

    public MongoDBEmployeeRepository(IConfiguration configuration, IMapper mapper)
    {
        var connectionString = configuration.GetConnectionString("EmployeeMongoDB") ?? throw new Exception("invalid connection string");
        var mongoClient = new MongoClient(connectionString);
        var database = mongoClient.GetDatabase("Employee");
        
        collection = database.GetCollection<Employee>("Employee");

        this.mapper = mapper;
    }
    
    public async Task<IEnumerable<Core.Models.Employee>> GetEmployees()
    {
        var employees = await collection
            .FindAsync(_ => true)
            .ConfigureAwait(false);

        return employees.ToList()
            .Select(employee => mapper.Map<Core.Models.Employee>(employee))
            .ToList();
    }

    public async Task<Core.Models.Employee?> GetEmployeeById(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<Employee>.Filter.Eq("_id", objectId);
        var employee = await collection
            .FindAsync(filter)
            .ConfigureAwait(false);
        
        return employee != null 
            ? mapper.Map<Core.Models.Employee>(employee.First()) 
            : null;
    }

    public async Task<string> CreateEmployee(Core.Models.Employee employee)
    {
        var employeeDBType = mapper.Map<Employee>(employee);
        
        await collection.InsertOneAsync(employeeDBType)
            .ConfigureAwait(false);

        return employeeDBType.Id.ToString();
    }
}