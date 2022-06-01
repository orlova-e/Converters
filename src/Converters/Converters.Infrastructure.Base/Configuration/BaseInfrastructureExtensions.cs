using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Converters.Infrastructure.Base.Configuration;

public static class BaseInfrastructureExtensions
{
    public static IServiceCollection AddBaseInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services
            .AddDbContext<Context>(option => option.UseNpgsql(connectionString));

        return services;
    }
}