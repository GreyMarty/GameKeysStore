using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Developers.GetDevelopersPaged;

internal class GetDevelopersPagedQueryHandler : IRequestHandler<GetDevelopersPagedQuery, IEnumerable<DeveloperReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetDevelopersPagedQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeveloperReadModel>> Handle(GetDevelopersPagedQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Developer>();
        options.OrderByAsc(x => x.Name);
        options.AsNoTracking();
        request.ConfigureOptions?.Invoke(options);

        var developers = await options.Apply(_db.Developers)
            .ToPagedListAsync(request.PageIndex, request.PageSize);

        return _mapper.Map<IEnumerable<DeveloperReadModel>>(developers);
    }
}
