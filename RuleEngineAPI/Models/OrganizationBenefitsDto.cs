namespace RuleEngineAPI.Models;

public class OrganizationBenefitsDto
{
    public string HciCode { get; set; }
    
    public IEnumerable<ResidentBenefitsDto> ResidentBenefits { get; set; }

    public double TotalServiceFee { get; set; }
}