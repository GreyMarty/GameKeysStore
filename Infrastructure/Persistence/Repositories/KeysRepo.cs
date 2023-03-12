using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class KeysRepo : RepoBase<Key>, IKeysRepo
{
    public KeysRepo(IApplicationDbContext context) :
        base(context, x => x.Keys)
    {
    }
}