using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class DevelopersRepo : RepoBase<Developer>, IDevelopersRepo
{
    public DevelopersRepo(IApplicationDbContext context)
        : base(context, c => c.Developers)
    {
    }
}