using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class KeyMappingProfile : Profile
{
    public KeyMappingProfile()
    {
        CreateMap<Key, KeyDto>();
        CreateMap<KeyDto, Key>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            ).ForMember(
                dest => dest.GameId,
                opt => opt.Ignore()
            ).ForMember(
                dest => dest.Purchased,
                opt => opt.Ignore()
            );
    }
}