namespace RuleEngineAPI.Core.Models;

public class OrganizationBenefits
{
    public OrganizationBenefits()
    {
        this.ResidentBenefits = new List<ResidentBenefits>();
    }
    
    public string HciCode { get; set; }
    
    public IEnumerable<ResidentBenefits> ResidentBenefits { get; set; }

    public double TotalServiceFee => this.ResidentBenefits.Sum(r => r.TotalServiceFee);
}