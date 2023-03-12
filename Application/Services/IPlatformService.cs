using Application.DTOs;
using Domain.Entities;

namespace Application.Services;

public interface IPlatformService
{
    public Platform GetOrCreate(PlatformDto model);
}