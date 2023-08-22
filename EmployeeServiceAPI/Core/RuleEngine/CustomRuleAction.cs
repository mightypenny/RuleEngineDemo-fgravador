using EmployeeService.Core.Models;
using RulesEngine.Actions;
using RulesEngine.Models;

namespace EmployeeService.Core.RuleEngine;

public class CustomRuleAction : ActionBase
{

    private readonly Employee employee;

    public CustomRuleAction(Employee employee)
    {
        this.employee = employee;
    }

    public override ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
    {
        var salary = context.GetContext<string>("customContextSalary");

        salary += employee.FirstName.Equals("Francis") ? 100 : 1;

        return new ValueTask<object>(salary);
    }
}