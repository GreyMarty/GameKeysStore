using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class KeyMappingProfile : Profile
{
    public KeyMappingProfile()
    {
        CreateMap<Key, KeyReadModel>()
            .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game.Name));
        CreateMap<KeyReadModel, KeyWriteModel>();
        CreateMap<KeysWriteModel, KeyWriteModel>();
        CreateMap<KeyWriteModel, Key>();
        CreateMap<KeysWriteModel, Key>();
    }
}