using System.Reflection;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

internal static class ServiceLocator
{
    public static void AddMarkedServices(this IServiceCollection services, Assembly assembly)
    {
        var servicesWithAttributes =
        (
            from t in assembly.GetTypes()
            select new ServiceAttributePair(t,
                t.GetCustomAttributes()
                    .FirstOrDefault(a => a.GetType() == typeof(ServiceAttribute)) as ServiceAttribute)
        ).Where(x => x.Attribute is not null);

        foreach (var serviceWithAttribute in servicesWithAttributes)
        {
            var implementationType = serviceWithAttribute.ServiceType;
            var serviceType = implementationType.GetInterfaces().FirstOrDefault();
            var lifetime = serviceWithAttribute.Attribute.Lifetime;

            services.Add(serviceType is null
                ? new ServiceDescriptor(implementationType, lifetime)
                : new ServiceDescriptor(serviceType, implementationType, lifetime)
            );
        }
    }
}

internal record ServiceAttributePair(Type ServiceType, ServiceAttribute Attribute);