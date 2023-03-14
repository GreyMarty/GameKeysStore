using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public interface IPlatformService
{
    public Platform GetOrCreate(PlatformDto model);
}

[Service(ServiceLifetime.Scoped)]
public class PlatformService : IPlatformService
{
    private readonly IPlatformsRepo _platformsRepo;
    private readonly IMapper _mapper;

    public PlatformService(IPlatformsRepo platformsRepo, IMapper mapper)
    {
        _platformsRepo = platformsRepo;
        _mapper = mapper;
    }

    public Platform GetOrCreate(PlatformDto model)
    {
        var platform = _platformsRepo.Get(e => e.Name == model.Name);

        if (platform is null) return _platformsRepo.Add(_mapper.Map<Platform>(model));

        return platform;
    }
}