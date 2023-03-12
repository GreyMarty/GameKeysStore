using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<Game, GameDto>();

        CreateMap<GameDto, Game>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            ).ForMember(
                dest => dest.Rating,
                opt => opt.Ignore()
            );
    }
}