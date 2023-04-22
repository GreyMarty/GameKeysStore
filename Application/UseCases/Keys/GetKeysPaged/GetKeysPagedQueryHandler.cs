using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Keys.GetKeysPaged;

internal class GetKeysPagedQueryHandler : IRequestHandler<GetKeysPagedQuery, IPagedList<KeyReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetKeysPagedQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IPagedList<KeyReadModel>> Handle(GetKeysPagedQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Key>();
        options.Include(x => x.Platform);
        options.OrderByAsc(x => x.Game.Name);
        options.AsNoTracking();
        request.ConfigureOptions?.Invoke(options);

        var keys = await options.Apply(_db.Keys)
            .ToPagedListAsync(request.PageIndex, request.PageSize);

        return _mapper.Map<IPagedList<KeyReadModel>>(keys);
    }
}
