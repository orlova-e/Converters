using Converters.Services.Implementation;
using Converters.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Converters.Services.Configuration;

public static class SharedServicesExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IDateTimeService, DateTimeService>()
            .AddSingleton<IFileManager, FileManager>();

        return services;
    }
}