using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

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
        options.Include(x => x.Developer);
        options.Include(x => x.RecommendedSystemRequirements);
        options.OrderByAsc(x => x.Name);
        options.AsNoTracking();
        request.ConfigureOptions?.Invoke(options);

        var games = await options.Apply(_db.Games)
            .ToPagedListAsync(request.PageIndex, request.PageSize);

        return _mapper.Map<IPagedList<GameReadModel>>(games);
    }
}
