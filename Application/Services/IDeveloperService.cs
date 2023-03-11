using Domain.Entities;

namespace Application.Services;

public interface IDeveloperService
{
    public Developer GetOrCreate(string name);
}