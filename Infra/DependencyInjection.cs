using Application.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class DependencyInjection
{
    public static void InjectInfra(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, MessageBroker>();
    }
}