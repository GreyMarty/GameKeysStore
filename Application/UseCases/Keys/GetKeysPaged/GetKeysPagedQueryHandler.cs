using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        request.ConfigureOptions?.Invoke(options);

        var dbKeys = _db.Keys
            .Include(x => x.Platform)
            .Include(x => x.Game)
            .OrderBy(x => x.Game.Name)
            .AsNoTracking();

        var keys = await options.Apply(dbKeys)
            .ToPagedListAsync(request.PageIndex, request.PageSize);

        return _mapper.Map<IPagedList<KeyReadModel>>(keys);
    }
}
