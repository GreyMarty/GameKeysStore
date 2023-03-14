using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class UsersRepo : RepoBase<User>, IUsersRepo
{
    public UsersRepo(IApplicationDbContext context) :
        base(context, x => x.Users)
    {
    }
}