using Application.DTOs;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using OneOf.Types;

namespace Application.Services;

[Service(ServiceLifetime.Scoped)]
public class KeyService : IKeyService
{
    private readonly IKeysRepo _keysRepo;
    private readonly IGamesRepo _gamesRepo;
    private readonly IPlatformService _platformService;

    private readonly IMapper _mapper;

    public KeyService(
        IKeysRepo keysRepo,
        IGamesRepo gamesRepo,
        IPlatformService platformService,
        IMapper mapper)
    {
        _keysRepo = keysRepo;
        _gamesRepo = gamesRepo;
        _platformService = platformService;
        _mapper = mapper;
    }

    public IEnumerable<Key> GetAll()
    {
        return _keysRepo
            .GetAll(options =>
                options.Include(e => e.Platform)
            ).ToArray();
    }

    public IEnumerable<Key> GetAllFor(int gameId)
    {
        return _keysRepo
            .GetAll(options =>
                options.Include(e => e.Platform)
            ).Where(e => e.GameId == gameId)
            .ToArray();
    }

    public OneOf<Key, ValidationFailed, NotFound> Create(int gameId, KeyDto model)
    {
        if (!_gamesRepo.Any(e => e.Id == gameId)) return new NotFound();

        if (_keysRepo.Any(e => e.KeyString == model.KeyString))
            return new ValidationFailed(nameof(model.KeyString), $"Key '{model.KeyString}' already exists");

        model.Platform.Name = model.Platform.Name.ToLower();
        var platform = _platformService.GetOrCreate(model.Platform);

        var key = _mapper.Map<Key>(model);
        key.GameId = gameId;
        key.PlatformId = platform.Id;
        key.Platform = null!;

        return _keysRepo.Add(key);
    }

    public OneOf<Key, ValidationFailed, NotFound> Update(int keyId, KeyDto model)
    {
        if (_keysRepo.Any(e => e.Id != keyId && e.KeyString == model.KeyString))
            return new ValidationFailed(nameof(model.KeyString), $"Key '{model.KeyString}' already exists");

        var sourceKey = _keysRepo.Get(keyId);

        if (sourceKey is null) return new NotFound();

        model.Platform.Name = model.Platform.Name.ToLower();
        var platform = _platformService.GetOrCreate(model.Platform);

        var key = _mapper.Map(model, sourceKey);
        key.PlatformId = platform.Id;
        key.Platform = null!;

        return _keysRepo.Update(key)!;
    }

    public OneOf<Success, NotFound> Delete(int keyId)
    {
        if (_keysRepo.Remove(keyId)) return new Success();

        return new NotFound();
    }
}