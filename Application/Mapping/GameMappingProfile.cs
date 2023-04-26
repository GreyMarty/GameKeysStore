using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<Game, GameReadModel>()
            .ForMember(d => d.Images, opt =>
                opt.MapFrom(s => s.Images.Select(x => x.File.Path)));
        
        CreateMap<GameReadModel, GameWriteModel>()
            .ForMember(d => d.CategoryIds, opt =>
                opt.MapFrom(s => s.Categories.Select(c => c.Id)));
        
        CreateMap<GameWriteModel, Game>()
            .ForMember(d => d.Images, opt => 
                opt.Ignore());
    }
}