namespace RuleEngineAPI.Core.RuleEngine;

public static class ComplexRulesHelper
{
    // public static bool ComplexConditionServiceFeeByProcedureName(
    //     IEnumerable<BenefitCalculationMetaData> metaData, 
    //     string validProcedureNames)
    // {
    //     var procedureNames = validProcedureNames.Split(",");
    //     
    //     return metaData.Any(r => procedureNames.Contains(r.ProcedureName));
    // }
    //
    // public static IList<BenefitsByOrganization> ComplexCalculationServiceFeeByProcedureName(
    //     IEnumerable<BenefitCalculationMetaData> metaData, 
    //     string validProcedureNames,
    //     double serviceFee,
    //     double ceilingAmount)
    // {
    //     var procedureNames = validProcedureNames.Split(",");
    //     double totalServiceFee = 0;
    //
    //     var result = new List<BenefitsByOrganization>();
    //     metaData.ToList().ForEach(data =>
    //     {
    //         if (!procedureNames.Contains(data.ProcedureName) ||
    //             ceilingAmount <= 0)
    //         {
    //             return;
    //         }
    //
    //         totalServiceFee += ceilingAmount >= serviceFee ? serviceFee : ceilingAmount - serviceFee;
    //         ceilingAmount -= serviceFee;
    //
    //         if (result.Exists(r => r.HciCode.Equals(data.HciCode)))
    //         {
    //             result.First(r => r.HciCode.Equals(data.HciCode)).TotalServiceFee += serviceFee;
    //         }
    //         else
    //         {
    //             result.Add(new BenefitsByOrganization
    //             {
    //                 HciCode = data.HciCode,
    //                 TotalServiceFee = serviceFee
    //             });
    //         }
    //     });
    //
    //     return result;
    // }
}