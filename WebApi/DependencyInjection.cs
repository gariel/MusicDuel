using Microsoft.AspNetCore.Authorization;

namespace WebApi;

public static class DependencyInjection
{
    public static void InjectWebApi(this IServiceCollection services)
    {
        services.AddTransient<AuthorizationMiddleware>();
    }
}