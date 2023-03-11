using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // services.AddScoped<IGamesService, GameService>();
        // services.AddScoped<IDeveloperService, DeveloperService>();

        services.AddMarkedServices(assembly);

        services.AddAutoMapper(options => { options.AddMaps(assembly); });

        services.AddValidatorsFromAssembly(assembly);
    }
}