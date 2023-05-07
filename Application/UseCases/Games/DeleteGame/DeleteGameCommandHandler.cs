using MediatR;

namespace Application.UseCases.Games.DeleteGame;

internal class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
{
    private readonly IApplicationDbContext _db;

    public DeleteGameCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _db.Games.FindAsync(request.Id);

        if (game is null)
        {
            return;
        }

        game.Deleted = true;
        _db.Games.Update(game);
        await _db.SaveChangesAsync();
    }
}
