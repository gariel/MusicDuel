using System.Reflection;
using Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void InjectApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserInfo, UserInfo>();
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).GetTypeInfo().Assembly));
    }
}