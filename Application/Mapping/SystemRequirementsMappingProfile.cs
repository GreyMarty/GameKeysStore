using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class SystemRequirementsMappingProfile : Profile
{
    public SystemRequirementsMappingProfile()
    {
        CreateMap<SystemRequirements, SytemRequirementsReadModel>();
        CreateMap<SytemRequirementsWriteModel, SystemRequirements>();
    }
}