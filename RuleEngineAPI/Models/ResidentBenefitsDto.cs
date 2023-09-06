namespace RuleEngineAPI.Models;

public class ResidentBenefitsDto
{
    public string Nric { get; set; }
    
    public IEnumerable<ResidentProcedureDto> Procedures { get; set; }

    public double TotalServiceFee { get; set; }
}