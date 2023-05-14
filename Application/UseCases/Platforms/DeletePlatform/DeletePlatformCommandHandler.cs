
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.Platforms.DeletePlatform;

internal class DeletePlatformCommandHandler : IRequestHandler<DeletePlatformCommand>
{
    private readonly IApplicationDbContext _db;

    public DeletePlatformCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Handle(DeletePlatformCommand request, CancellationToken cancellationToken)
    {
        var platform = await _db.Platforms.FindAsync(request.Id);

        if (platform is null) 
        {
            throw new EntityDoesNotExistException();
        }

        _db.Platforms.Remove(platform);
        await _db.SaveChangesAsync();
    }
}
