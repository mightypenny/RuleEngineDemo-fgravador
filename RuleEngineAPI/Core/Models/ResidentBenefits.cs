namespace RuleEngineAPI.Core.Models;

public class ResidentBenefits
{
    public ResidentBenefits()
    {
        this.Procedures = new List<ResidentProcedure>();
    }
    
    public string Nric { get; set; }
    
    public IEnumerable<ResidentProcedure> Procedures { get; set; }

    public double TotalServiceFee => this.Procedures.Sum(r => r.ServiceFee);
}