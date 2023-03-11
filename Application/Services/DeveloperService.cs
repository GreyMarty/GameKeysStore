using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

[Service(ServiceType.Scoped)]
public class DeveloperService : IDeveloperService
{
    private readonly IDevelopersRepo _developersRepo;

    public DeveloperService(IDevelopersRepo developersRepo)
    {
        _developersRepo = developersRepo;
    }

    public Developer GetOrCreate(string name)
    {
        var developer = _developersRepo.Get(x => x.Name == name);

        if (developer is not null) return developer;

        return _developersRepo.Add(new Developer { Name = name });
    }
}