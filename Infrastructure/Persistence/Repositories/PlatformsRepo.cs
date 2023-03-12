using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class PlatformsRepo : RepoBase<Platform>, IPlatformsRepo
{
    public PlatformsRepo(IApplicationDbContext context) :
        base(context, x => x.Platforms)
    {
    }
}