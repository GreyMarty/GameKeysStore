using Application.Exceptions;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Keys.CreateKey;

internal class CreateKeyCommandHandler : IRequestHandler<CreateKeyCommand, KeyReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateKeyCommandHandler(IApplicationDbContext db, IMapper mapper, IMediator mediator)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<KeyReadModel> Handle(CreateKeyCommand request, CancellationToken cancellationToken)
    {
        if (_db.Keys.Any(x => x.KeyString == request.Key.KeyString)) 
        {
            throw new EntityAlreadyExistsException("Such key already exists", "Key.KeyString");
        }

        var platformId = await _db.Platforms
            .Where(x => x.Name == request.Key.Platform.Name)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        var key = _mapper.Map<Key>(request.Key);

        if (platformId != 0) 
        {
            key.Platform = null!;
            key.PlatformId = platformId;
        }

        await _db.Keys.AddAsync(key);
        await _db.SaveChangesAsync();

        return _mapper.Map<KeyReadModel>(key);
    }
}
