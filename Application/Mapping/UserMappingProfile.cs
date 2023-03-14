using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            ).ForMember(
                dest => dest.RegisteredOn,
                opt => opt.Ignore()
            );
    }
}