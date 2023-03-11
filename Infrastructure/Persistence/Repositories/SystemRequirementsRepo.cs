using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class SystemRequirementsRepo : RepoBase<SystemRequirements>, ISystemRequirementsRepo
{
    public SystemRequirementsRepo(IApplicationDbContext context)
        : base(context, c => c.SystemRequirements)
    {
    }
}