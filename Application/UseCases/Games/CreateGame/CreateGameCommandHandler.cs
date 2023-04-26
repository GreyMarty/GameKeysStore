using Application.Exceptions;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Games.CreateGame;

internal class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateGameCommandHandler(IApplicationDbContext db, IMapper mapper, IMediator mediator)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GameReadModel> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        if (_db.Games.Any(x => x.Name == request.Game.Name))
        {
            throw new EntityAlreadyExistsException("Game with the same name already exists", "Game.Name");
        }

        var developerId = await _db.Developers
            .Where(x => x.Name == request.Game.Developer.Name)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        var game = _mapper.Map<Game>(request.Game);
        
        if (developerId != 0) 
        {
            game.Developer = null!;
            game.DeveloperId = developerId;
        }

        game.Categories = request.Game.CategoryIds
            .Select(x => _db.Categories.Find(x))
            .Where(x => x is not null)
            .ToList()!;

        game.Images = new List<Image>();

        foreach (var imagePath in request.Game.Images) 
        {
            var image = await _db.Images.FirstOrDefaultAsync(x => x.File.Path == imagePath);

            if (image is null) 
            {
                var fileId = await _db.Files
                    .Where(x => x.Path == imagePath)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                image = new Image { FileId = fileId };
            }
            
            game.Images.Add(image);
        }

        await _db.Games.AddAsync(game);
        await _db.SaveChangesAsync();

        return _mapper.Map<GameReadModel>(game);
    }
}
