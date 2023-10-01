using Application.Common.Interfaces;
using Application.Common.Messaging;
using Infra.Repositories;
using Infra.Security;
using Infra.Spotify;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class DependencyInjection
{
    public static void InjectInfra(this IServiceCollection services)
    {
        services.AddTransient<ISecurityInfoProvider, SecurityEnvInfoProvider>();
        services.AddTransient<IHashService, HashService>();
        services.AddTransient<IDatabaseInfoProvider, DatabaseEnvInfoProvider>();
        services.AddSingleton<IMessageBroker, MessageBroker>();
        services.AddTransient<IEnvProvider, EnvironmentVariableProvider>();
        services.AddTransient<IUsers, UserRepository>();
        services.AddTransient<IRooms, RoomRepository>();
        services.AddTransient<ISpotifyMusic, SpotifyMusicResolver>();
    }
}