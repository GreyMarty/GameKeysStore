using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Platforms.GetPlatformsPaged;

internal class GetPlatformsPagedQueryHandler : IRequestHandler<GetPlatformsPagedQuery, IPagedList<PlatformReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetPlatformsPagedQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Task<IPagedList<PlatformReadModel>> Handle(GetPlatformsPagedQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
