using Application.Exceptions;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Platforms.CreatePlatform;

internal class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, PlatformReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreatePlatformCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PlatformReadModel> Handle(CreatePlatformCommand request, CancellationToken cancellationToken)
    {
        if (_db.Platforms.Any(x => x.Name == request.Platform.Name))
        {
            throw new EntityAlreadyExistsException("Platform with the same name already exists", "Platform.Name");
        }

        var platform = _mapper.Map<Platform>(request.Platform);

        await _db.Platforms.AddAsync(platform);
        await _db.SaveChangesAsync();

        return _mapper.Map<PlatformReadModel>(platform);
    }
}
