using Application.Common.Interfaces;
using Infra;
using WebApi.Middleware;

namespace WebApi;

public static class DependencyInjection
{
    public static void InjectWebApi(this IServiceCollection services)
    {
        services.AddTransient<AuthorizationMiddleware>();
        services.AddTransient<ISerializer, Serializer>();
    }
}