using Application.Models.ReadModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Games.GetGame;

internal class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetGameQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GameReadModel> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        var game = await _db.Games
            .Include(x => x.Developer)
            .Include(x => x.RecommendedSystemRequirements)
            .Include(x => x.Images)
            .Include(x => x.Categories)
            .Include(x => x.Images)
            .ThenInclude(x => x.File)
            .AsNoTracking()
            .FirstOrDefaultAsync(request.Predicate);

        return _mapper.Map<GameReadModel>(game);
    }
}
