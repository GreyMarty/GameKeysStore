using System.Reflection;
using Application;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IGamesRepo, GamesRepo>();
        services.AddScoped<IDevelopersRepo, DevelopersRepo>();
        services.AddScoped<ISystemRequirementsRepo, SystemRequirementsRepo>();
        services.AddScoped<IKeysRepo, KeysRepo>();
        services.AddScoped<IPlatformsRepo, PlatformsRepo>();
        services.AddScoped<IUsersRepo, UsersRepo>();

        services.AddMarkedServices(Assembly.GetExecutingAssembly());
    }
}