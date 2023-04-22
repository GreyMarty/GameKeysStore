using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class DeveloperMappingProfile : Profile
{
    public DeveloperMappingProfile()
    {
        CreateMap<Developer, DeveloperReadModel>();
        CreateMap<DeveloperWriteModel, Developer>();
    }
}