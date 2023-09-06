using RuleEngineAPI.Core.Models;

namespace RuleEngineAPI.Core.Builders;

public class OrganizationBenefitsBuilder : IOrganizationBenefitsBuilder
{
    private readonly List<OrganizationBenefits> organizationBenefits = new();
    
    public IOrganizationBenefitsBuilder Build(IEnumerable<Procedure> procedures)
    {
        var proceduresByGp = procedures
            .AsEnumerable()
            .GroupBy(r => r.HciCode)
            .ToList();
            
        proceduresByGp
            .ForEach(organization =>
            {
                organizationBenefits.Add(new OrganizationBenefits
                {
                    HciCode = organization.Key,
                    ResidentBenefits = BuildResidentBenefits(organization.ToList())
                });
            });

        return this;
    }

    public List<OrganizationBenefits> Result()
    {
        return organizationBenefits;
    }

    private static IEnumerable<ResidentBenefits> BuildResidentBenefits(IEnumerable<Procedure> procedures)
    {
        var proceduresByResident = procedures
            .AsEnumerable()
            .GroupBy(r => r.Nric)
            .ToList();
        
        return proceduresByResident
            .Select(resident => new ResidentBenefits
            {
                Nric = resident.Key,
                Procedures = BuildProceduresByResident(resident.ToList())
            })
            .ToList();
    }

    private static IEnumerable<ResidentProcedure> BuildProceduresByResident(IEnumerable<Procedure> procedures)
    {
        return procedures
            .AsEnumerable()
            .Select(procedure => new ResidentProcedure
            {
                Name = procedure.Name,
                ServiceFee = procedure.ServiceFee
            })
            .ToList();
    }
}