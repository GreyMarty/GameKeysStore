using Domain.Entities;

namespace Application.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();
}