using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class GamesRepo : RepoBase<Game>, IGamesRepo
{
    public GamesRepo(IApplicationDbContext context)
        : base(context, c => c.Games)
    {
    }
}