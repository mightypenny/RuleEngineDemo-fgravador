namespace RuleEngineAPI.Models;

public class ProcedureDto
{
    public string HciCode { get; set; }
    
    public string Nric { get; set; }
    
    // Non-Chronic Enrollee
    // Chronic Enrollee (non-DHL)
    // Chronic Enrollee (DHL)
    // Vaccination
    // Screening
    public string Name { get; set; }
}