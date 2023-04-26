using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Games.UpdateGame;

internal class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, GameReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateGameCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GameReadModel> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _db.Games
            .Include(x => x.Developer)
            .Include(x => x.RecommendedSystemRequirements)
            .Include(x => x.Images)
            .Include(x => x.Categories)
            .Include(x => x.Images)
            .ThenInclude(x => x.File)
            .FirstOrDefaultAsync(x => x.Id == request.GameId);

        if (game is null) 
        {
            throw new EntityDoesNotExistException();
        }

        var developer = await _db.Developers.FirstOrDefaultAsync(x => x.Name == request.Game.Developer.Name);

        if (developer is null) 
        {
            game.DeveloperId = 0;
            game.Developer = new Developer();
        }

        _mapper.Map(request.Game, game);

        if (developer is not null)
        {
            game.DeveloperId = developer.Id;
            game.Developer = developer;
        }

        var categories = new Category[game.Categories.Count];
        game.Categories.CopyTo(categories, 0);

        foreach (var category in categories)
        {
            if (!request.Game.CategoryIds.Any(x => x == category.Id))
            {
                game.Categories.Remove(category);
            }
        }

        foreach (var categoryId in request.Game.CategoryIds) 
        {
            if (!game.Categories.Any(x => x.Id == categoryId)) 
            {
                var category = await _db.Categories.FindAsync(categoryId);

                if (category is not null)
                {
                    game.Categories.Add(category);
                }
            }
        }

        var images = new Image[game.Images.Count];
        game.Images.CopyTo(images, 0);

        foreach (var image in images)
        {
            if (!request.Game.Images.Any(x => x == image.File.Path))
            {
                game.Images.Remove(image);
                _db.Images.Remove(image);
            }
        }

        foreach (var imagePath in request.Game.Images)
        {
            if (!game.Images.Any(x => x.File is not null && x.File.Path == imagePath)) 
            {
                var fileId = await _db.Files
                    .Where(x => x.Path == imagePath)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                game.Images.Add(new Image { FileId = fileId });
            }
        }

        _db.Games.Update(game);
        await _db.SaveChangesAsync();

        return _mapper.Map<GameReadModel>(game);
    }
}
