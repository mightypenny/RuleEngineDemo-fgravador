using EmployeeService.Core.Models;

namespace EmployeeService.Core.RuleEngine;

public static class ComplexRulesHelper
{
    public static bool ComplexCondition(Employee employee)
    {
        return true;
    }

    public static double ComplexCalculation(Employee employee)
    {
        return 0;
    }
}