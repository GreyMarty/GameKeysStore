using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.Keys.DeleteKey;

internal class DeleteKeyCommandHandler : IRequestHandler<DeleteKeyCommand>
{
    private readonly IApplicationDbContext _db;

    public DeleteKeyCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Handle(DeleteKeyCommand request, CancellationToken cancellationToken)
    {
        var key = await _db.Keys.FindAsync(request.Id);

        if (key is null) 
        {
            throw new EntityDoesNotExistException();
        }

        _db.Keys.Remove(key);
        await _db.SaveChangesAsync();
    }
}
