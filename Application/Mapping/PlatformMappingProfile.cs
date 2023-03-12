using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class PlatformMappingProfile : Profile
{
    public PlatformMappingProfile()
    {
        CreateMap<Platform, PlatformDto>();
        CreateMap<PlatformDto, Platform>();
    }
}