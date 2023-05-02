using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.UseCases.GetGamesPaged;

internal class GetGamesPagedQueryHandler : IRequestHandler<GetGamesPagedQuery, IPagedList<GameReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetGamesPagedQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IPagedList<GameReadModel>> Handle(GetGamesPagedQuery request, CancellationToken cancellationToken)
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
        .ToPagedListAsync(request.PageIndex, request.PageSize);

        return _mapper.Map<IPagedList<GameReadModel>>(games);
    }
}
