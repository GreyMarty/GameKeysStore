using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Platforms.GetPlatforms;

internal class GetPlatformsQueryHandler : IRequestHandler<GetPlatformsQuery, IEnumerable<PlatformReadModel>>
{
    private readonly IApplicationDbContext _db;

    public GetPlatformsQueryHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<PlatformReadModel>> Handle(GetPlatformsQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Platform>();
        options.AsNoTracking();
        options.OrderByAsc(x => x.Name);
        request.ConfigureOptions?.Invoke(options);

        var dbPlatforms = _db.Platforms
            .OrderBy(x => x.Name)
            .AsNoTracking();

        var platforms = await options.Apply(dbPlatforms)
            .Select(x => new PlatformReadModel() { Id = x.Id, Name = x.Name, GamesCount = x.Keys.GroupBy(x => x.GameId).Count() })
            .ToArrayAsync();

        return platforms;
    }
}
