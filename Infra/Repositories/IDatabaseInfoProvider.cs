namespace Infra.Repositories;

public interface IDatabaseInfoProvider
{
    string ConnectionString { get; }
}