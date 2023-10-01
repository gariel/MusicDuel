using System.Data;
using Npgsql;

namespace Infra.Repositories;

public abstract class BaseRepository : IDisposable
{
    protected BaseRepository(IDatabaseInfoProvider dbInfoProvider)
    {
        _dbInfoProvider = dbInfoProvider;
    }

    private readonly IDatabaseInfoProvider _dbInfoProvider;
    private IDbConnection? _db;
    protected IDbConnection DB
    {
        get
        {
            if (_db is null)
            {
                _db = new NpgsqlConnection(_dbInfoProvider.ConnectionString);
                _db.Open();
            }

            return _db;
        }
    }

    public void Dispose()
        => _db?.Dispose();
}