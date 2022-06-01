using Converters.Web.Hubs;
using Converters.Web.Services.Implementation;
using Converters.Web.Services.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Converters.Web.Configuration;

public static class WebExtensions
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var hubOptions = configuration
            .GetSection(nameof(HubOptions))
            .Get<HubOptions>();

        services
            .Configure<ValidationOptions>(configuration.GetSection(nameof(ValidationOptions)))
            .AddDistributedMemoryCache()
            .AddSession()
            .AddOptions()
            .AddMvc().Services
            .AddSignalR()
            .AddHubOptions<ConvertersHub>(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(hubOptions.ClientTimeoutInterval);
                options.KeepAliveInterval = TimeSpan.FromSeconds(hubOptions.KeepAliveInterval);
            });

        services
            .AddMediatR(typeof(Program).Assembly)
            .AddFluentValidation(f =>
                f.RegisterValidatorsFromAssemblyContaining<Program>())
            .AddTransient<ITranslator, Translator>()
            .AddAutoMapper(typeof(Program).Assembly)
            .AddHttpContextAccessor()
            .TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        return services;
    }
}