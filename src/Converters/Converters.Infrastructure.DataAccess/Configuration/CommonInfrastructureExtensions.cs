using Converters.Infrastructure.DataAccess.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Converters.Infrastructure.DataAccess.Configuration;

public static class CommonInfrastructureExtensions
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<IRepository, Repository>();

        return services;
    }
}