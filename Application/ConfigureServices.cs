using System.Reflection;
using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMarkedServices(assembly);

        services.AddAutoMapper(options => { options.AddMaps(assembly); });

        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}