using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public class ServiceAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; init; }

    public ServiceAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}