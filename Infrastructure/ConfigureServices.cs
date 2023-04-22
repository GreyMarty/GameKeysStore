using System.Reflection;
using Application;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddMarkedServices(Assembly.GetExecutingAssembly());
    }
}