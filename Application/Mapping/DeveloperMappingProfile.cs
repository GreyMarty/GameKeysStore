using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class DeveloperMappingProfile : Profile
{
    public DeveloperMappingProfile()
    {
        CreateMap<Developer, DeveloperDto>();
        CreateMap<DeveloperDto, Developer>();
    }
}