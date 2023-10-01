using Application.Common.Interfaces;

namespace Infra.Repositories;

class DatabaseEnvInfoProvider : IDatabaseInfoProvider
{
    private readonly IEnvProvider _envProvider;

    public DatabaseEnvInfoProvider(IEnvProvider envProvider)
    {
        _envProvider = envProvider;
    }

    public string ConnectionString => _envProvider.StringValue("DATABASE_CONNECTION_STRING");
}