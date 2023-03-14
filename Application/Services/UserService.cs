using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

[Service(ServiceLifetime.Scoped)]
public class UserService : IUserService
{
    private readonly IUsersRepo _usersRepo;

    public UserService(IUsersRepo usersRepo)
    {
        _usersRepo = usersRepo;
    }

    public IEnumerable<User> GetAll()
    {
        return _usersRepo.GetAll().ToArray();
    }
}