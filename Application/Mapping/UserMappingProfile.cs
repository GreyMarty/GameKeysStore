using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserReadModel>();
        CreateMap<UserWriteModel, User>()
            .ForMember(x => x.Password, opt => opt.Ignore());
    }
}