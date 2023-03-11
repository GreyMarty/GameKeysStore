namespace Application.Services;

public class ServiceAttribute : Attribute
{
    public ServiceType Type { get; init; }

    public ServiceAttribute(ServiceType type)
    {
        Type = type;
    }
}

public enum ServiceType
{
    Transient,
    Scoped,
    Singleton
}