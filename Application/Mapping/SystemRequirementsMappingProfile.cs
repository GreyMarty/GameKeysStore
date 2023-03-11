using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class SystemRequirementsMappingProfile : Profile
{
    public SystemRequirementsMappingProfile()
    {
        CreateMap<SystemRequirements, SystemRequirementsDto>();
        CreateMap<SystemRequirementsDto, SystemRequirements>();
    }
}