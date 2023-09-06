using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RuleEngineAPI.Core.Models;
using RuleEngineAPI.Core.Services;
using RuleEngineAPI.Models;

namespace RuleEngineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RuleEngineController : ControllerBase
{
    private readonly IRuleEngineService ruleEngineService;
    private readonly IMapper mapper;
    private readonly ILogger<RuleEngineController> logger;

    public RuleEngineController(
        IRuleEngineService ruleEngineService,
        IMapper mapper,
        ILogger<RuleEngineController> logger)
    {
        this.ruleEngineService = ruleEngineService;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpPost(Name = "CalculateBenefits")]
    public async Task<IEnumerable<OrganizationBenefitsDto>> CalculateBenefits(IEnumerable<ProcedureDto> requestBody)
    {
        var procedures = requestBody
            .ToList()
            .Select(r => mapper.Map<Procedure>(r))
            .ToList();
        
        var result = await ruleEngineService
            .CalculateBenefits(procedures)
            .ConfigureAwait(false);
        
        logger.LogInformation("Benefits successfully calculated");
        
        return result
            .AsEnumerable()
            .Select(organizationBenefit => mapper.Map<OrganizationBenefitsDto>(organizationBenefit))
            .ToList();
    }
}
