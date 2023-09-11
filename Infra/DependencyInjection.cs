using Application.Common.Interfaces;
using Application.Common.Messaging;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class DependencyInjection
{
    public static void InjectInfra(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, MessageBroker>();
        services.AddTransient<IRooms, RoomRepository>();
    }
}