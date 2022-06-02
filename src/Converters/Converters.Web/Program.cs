using Converters.Infrastructure.Base.Configuration;
using Converters.Infrastructure.DataAccess.Configuration;
using Converters.Services.Configuration;
using Converters.Web.Configuration;
using Converters.Web.Hubs;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddBaseInfrastructure(builder.Configuration)
    .AddCommonInfrastructure()
    .AddSharedServices()
    .AddWebServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app
    .UseForwardedHeaders(new ForwardedHeadersOptions 
        {ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto})
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapHub<ConvertersHub>("/hubs");
        endpoints.MapDefaultControllerRoute();
    });

app.Run();