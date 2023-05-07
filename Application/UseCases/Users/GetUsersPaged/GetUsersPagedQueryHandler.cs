using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Users.GetUsersPaged;

internal class GetUsersPagedQueryHandler : IRequestHandler<GetUsersPagedQuery, IPagedList<UserReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetUsersPagedQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IPagedList<UserReadModel>> Handle(GetUsersPagedQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<User>();
        request.ConfigureOptions?.Invoke(options);

        var dbUsers = _db.Users
            .OrderBy(x => x.Id)
            .AsNoTracking();

        var users = await options.Apply(dbUsers)
            .ToPagedListAsync(request.PageIndex, request.PageSize);

        return _mapper.Map<IPagedList<UserReadModel>>(users);
    }
}
