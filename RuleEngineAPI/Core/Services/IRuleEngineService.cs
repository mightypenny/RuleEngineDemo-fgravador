using RuleEngineAPI.Core.Models;

namespace RuleEngineAPI.Core.Services;

public interface IRuleEngineService
{
    public Task<IEnumerable<OrganizationBenefits>> CalculateBenefits(IEnumerable<Procedure> procedures);
}