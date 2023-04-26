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
    private readonly IMapper _mapper;

    public GetPlatformsQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlatformReadModel>> Handle(GetPlatformsQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Platform>();
        options.AsNoTracking();
        options.OrderByAsc(x => x.Name);
        request.ConfigureOptions?.Invoke(options);

        var platforms = await options.Apply(_db.Platforms)
            .ToArrayAsync();

        return _mapper.Map<IEnumerable<PlatformReadModel>>(platforms);
    }
}
