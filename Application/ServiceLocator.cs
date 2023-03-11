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
            var serviceType = serviceWithAttribute.ServiceType;
            var interfaceType = serviceType.GetInterfaces().FirstOrDefault();

            switch (serviceWithAttribute.Attribute.Type)
            {
                case ServiceType.Scoped:
                    services.AddScoped(interfaceType, serviceType);
                    break;

                case ServiceType.Transient:
                    services.AddTransient(interfaceType, serviceType);
                    break;

                case ServiceType.Singleton:
                    services.AddSingleton(interfaceType, serviceType);
                    break;
            }
        }
    }
}

internal record ServiceAttributePair(Type ServiceType, ServiceAttribute Attribute);