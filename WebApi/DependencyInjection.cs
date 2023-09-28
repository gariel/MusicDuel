using Application.Common.Interfaces;
using Infra;
using WebApi.Middleware;
using WebApi.Security;

namespace WebApi;

public static class DependencyInjection
{
    public static void InjectWebApi(this IServiceCollection services)
    {
        services.AddTransient<ISerializer, Serializer>();

        services.AddTransient<IAuthenticationInfoProvider, AuthenticationInfoProvider>();
        services.AddTransient<IAuthenticationTokenManager, AuthenticationTokenManager>();
        
        services.AddTransient<AuthorizationMiddleware>();
    }
}