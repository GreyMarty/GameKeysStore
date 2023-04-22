using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class KeyMappingProfile : Profile
{
    public KeyMappingProfile()
    {
        CreateMap<Key, KeyReadModel>();
        CreateMap<KeyWriteModel, Key>();
    }
}