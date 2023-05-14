using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Keys.GetKeyGroupsSlice;

internal class GetKeyGroupsSliceQueryHandler : IRequestHandler<GetKeyGroupsSliceQuery, ISlice<KeyGroupReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetKeyGroupsSliceQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ISlice<KeyGroupReadModel>> Handle(GetKeyGroupsSliceQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Key>();
        request.ConfigureOptions?.Invoke(options);

        var dbKeys = _db.Keys
            .Where(x => !x.Game.Deleted)
            .AsNoTracking();

        var groups = await options.Apply(dbKeys)
            .Select(x => new { x.PlatformId, x.GameId })
            .Distinct()
            .OrderBy(x => x.GameId)
            .ToSliceAsync(request.Offset, request.Count, cancellationToken);

        var keyGroups = groups
            .Select(grp => new
            {
                Platform = _db.Platforms.Find(grp.PlatformId),
                GameInfo = _db.Games
                    .Select(g => new
                    {
                        Id = g.Id,
                        Name = g.Name,
                        PreviewImage = g.Images.Select(i => i.File.Path).FirstOrDefault(),
                        Categories = _mapper.Map<IEnumerable<CategoryReadModel>>(g.Categories.ToArray())
                    }).First(x => x.Id == grp.GameId),
                MinPrice = _db.Keys.Where(x => x.PlatformId == grp.PlatformId && x.GameId == grp.GameId).Min(x => x.Price)
            }).Select(x => new KeyGroupReadModel
            {
                Platform = _mapper.Map<PlatformReadModel>(x.Platform),
                GameId = x.GameInfo.Id,
                GameName = x.GameInfo.Name,
                PreviewImage = x.GameInfo.PreviewImage,
                Categories = x.GameInfo.Categories,
                MinPrice = x.MinPrice
            }).ToArray();

        return new Slice<KeyGroupReadModel>(keyGroups, request.Offset, groups.TotalCount);
    }
}
