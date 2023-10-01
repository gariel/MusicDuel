using Application.Common.Interfaces;
using Dapper;
using Domain;

namespace Infra.Repositories;

public class UserRepository : BaseRepository, IUsers
{
    private readonly IHashService _hashService;

    public UserRepository(IDatabaseInfoProvider dbInfoProvider, IHashService hashService)
        : base(dbInfoProvider)
    {
        _hashService = hashService;
    }

    public async Task<int?> ValidateUserAuthentication(string userName, string password)
    {
        var hashed = _hashService.Hash(password, userName);
        var user = await DB.QuerySingleOrDefaultAsync<User>(
            "select id, name from users where name = @userName and hashed_password = @hashed",
            new { userName, hashed });

        return user?.Id;
    }

    public async Task CreateUser(string userName, string password)
    {
        var hashed = _hashService.Hash(password, userName);
        await DB.ExecuteAsync(
            """
            insert into users (name, hashed_password)
            values (@userName, @hashed)
            """, new {userName, hashed});
    }
}