using MediatR;

namespace Application.UseCases.Developers.DeleteDeveloper;

internal class DeleteDeveloperCommandHandler : IRequestHandler<DeleteDeveloperCommand>
{
    private readonly IApplicationDbContext _db;

    public DeleteDeveloperCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
    {
        var developer = await _db.Developers.FindAsync(request.Id);

        if (developer is null) 
        {
            return;
        }

        _db.Developers.Remove(developer);
        await _db.SaveChangesAsync();
    }
}
