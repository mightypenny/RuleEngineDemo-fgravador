using RuleEngineAPI.Core.Models;

namespace RuleEngineAPI.Core.Builders;

public interface IOrganizationBenefitsBuilder
{
    IOrganizationBenefitsBuilder Build(IEnumerable<Procedure> procedures);

    List<OrganizationBenefits> Result();
}