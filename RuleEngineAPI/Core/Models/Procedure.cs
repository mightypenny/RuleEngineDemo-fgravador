namespace RuleEngineAPI.Core.Models;

public class Procedure
{
    private double serviceFee = 0;
    
    public string HciCode { get; set; }
    
    public string Nric { get; set; }
    
    public string Name { get; set; }

    public double ServiceFee => serviceFee;

    public void AddServiceFee(double ruleFee)
    {
        serviceFee += ruleFee;
    }
}