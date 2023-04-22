using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<Game, GameReadModel>();
        CreateMap<GameWriteModel, Game>();
    }
}