using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class RegistrationUserMappingProfile : Profile
{
    public RegistrationUserMappingProfile()
    {
        CreateMap<RegistrationUserDto, User>()
            .ForMember(
                dest => dest.Password,
                opt => opt.Ignore()
            ).ForMember(
                dest => dest.RegisteredOn,
                opt => opt.MapFrom(_ => DateTime.UtcNow)
            );
    }
}