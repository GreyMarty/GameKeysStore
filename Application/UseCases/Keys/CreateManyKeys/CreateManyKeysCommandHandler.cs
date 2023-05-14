using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Keys.CreateManyKeys;

internal class CreateManyKeysCommandHandler : IRequestHandler<CreateManyKeysCommand>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateManyKeysCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task Handle(CreateManyKeysCommand request, CancellationToken cancellationToken)
    {
        var platform = await _db.Platforms.FirstOrDefaultAsync(x => x.Name == request.Keys.Platform.Name);

        var game = await _db.Games.FirstOrDefaultAsync(x => x.Name == request.Keys.GameName);

        if (game is null) 
        {
            throw new EntityDoesNotExistException("Game with this name does not exist.");
        }

        if (platform is null) 
        {
            platform = _mapper.Map<Platform>(request.Keys.Platform);
            await _db.Platforms.AddAsync(platform);
        }

        foreach (var keyString in request.Keys.KeyStrings) 
        {
            if (string.IsNullOrWhiteSpace(keyString)) 
            {
                continue;
            }

            var key = _mapper.Map<Key>(request.Keys);
            key.KeyString = keyString;
            key.Platform = platform;
            key.Game = game;

            if (!_db.Keys.Any(x => x.KeyString == key.KeyString))
            {
                await _db.Keys.AddAsync(key);
            }
        }

        await _db.SaveChangesAsync();
    }
}
