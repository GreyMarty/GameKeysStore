using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class PlatformMappingProfile : Profile
{
    public PlatformMappingProfile()
    {
        CreateMap<Platform, PlatformReadModel>();
        CreateMap<PlatformWriteModel, Platform>();
    }
}