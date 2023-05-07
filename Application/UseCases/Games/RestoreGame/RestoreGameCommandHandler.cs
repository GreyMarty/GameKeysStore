using MediatR;

namespace Application.UseCases.Games.RestoreGame;

internal class RestoreGameCommandHandler : IRequestHandler<RestoreGameCommand>
{
    private readonly IApplicationDbContext _db;

    public RestoreGameCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Handle(RestoreGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _db.Games.FindAsync(request.Id);

        if (game is null) 
        {
            return;
        }

        game.Deleted = false;
        _db.Games.Update(game);
        await _db.SaveChangesAsync();
    }
}
