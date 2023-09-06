using AutoMapper;
using RuleEngineAPI.Core.Models;
using RuleEngineAPI.Models;

namespace RuleEngineAPI.Mappers;

public class RuleEngineDataProfile : Profile
{
    public RuleEngineDataProfile()
    {
        // DTO to Domain
        CreateMap<ProcedureDto, Procedure>();
        
        // Domain to DTO
        CreateMap<OrganizationBenefits, OrganizationBenefitsDto>();
        CreateMap<ResidentBenefits, ResidentBenefitsDto>();
        CreateMap<ResidentProcedure, ResidentProcedureDto>();
    }
}