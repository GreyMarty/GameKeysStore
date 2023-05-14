using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Games.GetGamesSlice;

internal class GetGamesSliceQueryHandler : IRequestHandler<GetGamesSliceQuery, ISlice<GameReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetGamesSliceQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ISlice<GameReadModel>> Handle(GetGamesSliceQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Game>();
        request.ConfigureOptions?.Invoke(options);

        var dbGames = _db.Games
            .Include(x => x.Developer)
            .Include(x => x.RecommendedSystemRequirements)
            .Include(x => x.Categories)
            .Include(x => x.Images)
            .ThenInclude(x => x.File)
            .OrderBy(x => x.Name)
            .AsNoTracking();

        var games = await options.Apply(dbGames)
            .Skip(request.Offset)
            .Take(request.Count)
            .ToSliceAsync(request.Offset, request.Count);

        return _mapper.Map<ISlice<GameReadModel>>(games);
    }
}
