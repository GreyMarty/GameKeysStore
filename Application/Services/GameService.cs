using Application.DTOs;
using Application.Results;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using OneOf.Types;

namespace Application.Services;

[Service(ServiceLifetime.Scoped)]
public class GameService : IGamesService
{
    private readonly IGamesRepo _gamesRepo;
    private readonly ISystemRequirementsRepo _systemRequirementsRepo;
    private readonly IDeveloperService _developerService;

    private readonly IValidator<GameDto> _gameValidator;

    private readonly IMapper _mapper;

    public GameService(
        IGamesRepo gamesRepo,
        ISystemRequirementsRepo systemRequirementsRepo,
        IDeveloperService developerService,
        IMapper mapper,
        IValidator<GameDto> gameValidator)
    {
        _gamesRepo = gamesRepo;
        _developerService = developerService;
        _mapper = mapper;
        _gameValidator = gameValidator;
        _systemRequirementsRepo = systemRequirementsRepo;
    }

    public IEnumerable<Game> GetAll()
    {
        return _gamesRepo
            .GetAll(options =>
                options
                    .Include(e => e.Developer)
                    .Include(e => e.RecommendedSystemRequirements)
                    .Include(e => e.Keys)
            )
            .ToArray();
    }

    public OneOf<Game, ValidationFailed> Create(GameDto model)
    {
        if (_gamesRepo.Any(x => x.Name == model.Name))
            return new ValidationFailed(nameof(model.Name), "Game with the same name already exists");

        var validationResult = _gameValidator.Validate(model);

        if (!validationResult.IsValid)
            return new ValidationFailed(_mapper.Map<IEnumerable<ValidationError>>(validationResult.Errors));

        var developer = _developerService.GetOrCreate(model.Developer.Name);

        var game = _mapper.Map<Game>(model);

        return _gamesRepo.Add(game);
    }

    public OneOf<Game, ValidationFailed, NotFound> Update(int id, GameDto model)
    {
        if (_gamesRepo.Any(x => x.Id != id && x.Name == model.Name))
            return new ValidationFailed(nameof(model.Name), "Game with the same name already exists");

        var validationResult = _gameValidator.Validate(model);

        if (!validationResult.IsValid)
            return new ValidationFailed(_mapper.Map<IEnumerable<ValidationError>>(validationResult.Errors));

        var sourceGame = _gamesRepo.Get(id, options =>
            options.Include(e => e.RecommendedSystemRequirements)
        );

        if (sourceGame is null)
            return new NotFound();

        var developer = _developerService.GetOrCreate(model.Developer.Name);

        var systemRequirements = _mapper.Map(
            model.RecommendedSystemRequirements,
            sourceGame.RecommendedSystemRequirements
        );

        var game = _mapper.Map(model, sourceGame);
        game.DeveloperId = 0;

        return _gamesRepo.Update(game)!;
    }

    public OneOf<Success, NotFound> Delete(int gameId)
    {
        var systemRequirementsId = (
            from game in _gamesRepo.GetAll()
            where game.Id == gameId
            select game.RecommendedSystemRequirementsId
        ).SingleOrDefault();

        if (_gamesRepo.Remove(gameId))
        {
            _systemRequirementsRepo.Remove(systemRequirementsId);
            return new Success();
        }

        return new NotFound();
    }
}