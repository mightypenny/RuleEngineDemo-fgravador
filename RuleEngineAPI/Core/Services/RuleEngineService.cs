using Newtonsoft.Json;
using RuleEngineAPI.Core.Builders;
using RuleEngineAPI.Core.Models;
using RuleEngineAPI.Core.RuleEngine;
using RulesEngine.Models;

namespace RuleEngineAPI.Core.Services;

public class RuleEngineService : IRuleEngineService
{
    private readonly IOrganizationBenefitsBuilder organizationBenefitsBuilder;

    public RuleEngineService(IOrganizationBenefitsBuilder organizationBenefitsBuilder)
    {
        this.organizationBenefitsBuilder = organizationBenefitsBuilder;
    }
    
    public async Task<IEnumerable<OrganizationBenefits>> CalculateBenefits(IEnumerable<Procedure> procedures)
    {
        procedures = procedures.ToList();
        
        await CalculateFixAmount(procedures)
            .ConfigureAwait(false);

        await CalculateVariableAmount(procedures)
            .ConfigureAwait(false);
        
        return organizationBenefitsBuilder
            .Build(procedures)
            .Result();
    }

    private static async Task CalculateFixAmount(IEnumerable<Procedure> procedures)
    {
        var ruleEngine = await InitializeRuleEngine(@"./Core/RuleEngine/FixAmountRule.json")
            .ConfigureAwait(false);

        async void CalculateAction(Procedure procedure)
        {
            var parameter1 = new RuleParameter("procedureInfo", procedure);
            var results = await ruleEngine
                .ExecuteAllRulesAsync("FixAmountServiceFeeFlow", parameter1)
                .ConfigureAwait(false);

            results
                .Where(r => r.IsSuccess)
                .ToList()
                .ForEach(result =>
                {
                    procedure.AddServiceFee((double)result.ActionResult.Output);
                });
        }

        procedures.ToList().ForEach(CalculateAction);
    }
    
    private static async Task CalculateVariableAmount(IEnumerable<Procedure> procedures)
    {
        var ruleEngine = await InitializeRuleEngine(@"./Core/RuleEngine/VariableAmountRule.json")
            .ConfigureAwait(false);

        var totalAmountUsedPerGp = new Dictionary<string, double>();

        async void CalculateAction(Procedure procedure)
        {
            totalAmountUsedPerGp.TryAdd(procedure.HciCode, 0.0);
            
            var parameter1 = new RuleParameter("procedureInfo", procedure);
            var parameter2 = new RuleParameter("totalAmountUsedInfo", totalAmountUsedPerGp[procedure.HciCode]);

            var results = await ruleEngine
                .ExecuteAllRulesAsync("VariableAmountServiceFeeFlow", parameter1, parameter2)
                .ConfigureAwait(false);

            results
                .Where(r => r.IsSuccess)
                .ToList()
                .ForEach(result =>
                {
                    var serviceFee = (double)result.ActionResult.Output;
                    procedure.AddServiceFee(serviceFee);
                    totalAmountUsedPerGp[procedure.HciCode] += serviceFee;
                });
        }

        procedures.ToList().ForEach(CalculateAction);
    }

    private static async Task<RulesEngine.RulesEngine> InitializeRuleEngine(string fileName)
    {
        var config = await File
            .ReadAllTextAsync(fileName)
            .ConfigureAwait(false);

        var workflows = JsonConvert.DeserializeObject<List<Workflow>>(config);
        var settings = new ReSettings
        {
            CustomTypes = new[] { typeof(ComplexRulesHelper) }
        };

        return new RulesEngine.RulesEngine(workflows!.ToArray(), settings);
    }
}