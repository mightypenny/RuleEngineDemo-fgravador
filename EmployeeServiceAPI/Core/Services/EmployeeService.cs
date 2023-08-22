using System.Text;
using EmployeeService.Core.RuleEngine;
using EmployeeService.Repositories;
using EmployeeService.Repositories.MongoDB;
using EmployeeService.Repositories.Postgres;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RulesEngine.Actions;
using RulesEngine.Models;
using Employee = EmployeeService.Core.Models.Employee;

namespace EmployeeService.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeService(
        IEnumerable<IEmployeeRepository> employeeRepositories,
        IOptions<AppSettings> appSettings)
    {
        employeeRepository = appSettings.Value.DBType.Equals("MongoDB") 
            ? employeeRepositories.First(r => r.GetType() == typeof(MongoDBEmployeeRepository)) 
            : employeeRepositories.First(r => r.GetType() == typeof(PostgresDBEmployeeRepository));
    }
    
    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        var employees = await employeeRepository
            .GetEmployees()
            .ConfigureAwait(false);

        return employees;
    }

    public async Task<Employee> GetEmployeeById(string id)
    {
        var employee = await employeeRepository
            .GetEmployeeById(id)
            .ConfigureAwait(false);

        if (employee == null)
        {
            throw new InvalidDataException($"Employee Id {id} not found");
        }
        
        return employee;
    }

    public async Task<string> CreateEmployee(Employee employee)
    {
        // validate here
        return await test(employee);

        // return await employeeRepository
        //     .CreateEmployee(employee)
        //     .ConfigureAwait(false);
    }

    private async Task<string> test(Employee employee)
    {
        var config = await File
            .ReadAllTextAsync("./Core/RuleEngine/BenefitsRules.json")
            .ConfigureAwait(false);
        
        var workflows = JsonConvert.DeserializeObject<List<Workflow>>(config);
        var settings = new ReSettings
        {
            CustomActions = new Dictionary<string, Func<ActionBase>>
            {
                { "CustomRuleAction", () => new CustomRuleAction(employee) }
            },
            CustomTypes = new[] { typeof(ComplexRulesHelper) }
        };
        
        var ruleEngine = new RulesEngine.RulesEngine(workflows.ToArray(), settings);
        
        var rp1 = new RuleParameter("employeeInfo", employee);
        
        var resultList  = await ruleEngine
            .ExecuteAllRulesAsync("BonusRule", rp1)
            .ConfigureAwait(false);

        var sb = new StringBuilder();
        foreach (var result in resultList)
        {
            sb.Append($"Rule - {result.Rule.RuleName}, IsSuccess - {result.IsSuccess}\n");
            sb.Append($"Output bonus - {result.ActionResult.Output}\n");
        }

        return sb.ToString();
    }
}