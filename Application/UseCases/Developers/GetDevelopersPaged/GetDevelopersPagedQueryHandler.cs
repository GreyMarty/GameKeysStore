using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Developers.GetDevelopersPaged;

internal class GetDevelopersPagedQueryHandler : IRequestHandler<GetDevelopersPagedQuery, IPagedList<DeveloperReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetDevelopersPagedQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IPagedList<DeveloperReadModel>> Handle(GetDevelopersPagedQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Developer>();
        request.ConfigureOptions?.Invoke(options);

        var dbDevelopers = _db.Developers
            .OrderBy(x => x.Name)
            .AsNoTracking();

        var developers = await options.Apply(dbDevelopers)
            .Select(x => new DeveloperReadModel { Id = x.Id, Name = x.Name, GamesCount = x.Games.Count() })
            .ToPagedListAsync(request.PageIndex, request.PageSize);

        return developers;
    }
}
