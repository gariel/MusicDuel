using Application.Common.Interfaces;
using Dapper;
using Domain;

namespace Infra.Repositories;

public class RoomRepository : BaseRepository, IRooms
{
    public RoomRepository(IDatabaseInfoProvider dbInfoProvider)
        : base(dbInfoProvider) {}
    
    public Task<int> InsertAsync(RoomInfo room)
    {
        return DB.ExecuteScalarAsync<int>(
            """
            insert into rooms (title, is_playing, game_type, visibility_type, rounds, playlist_code)
            values (@Title, @IsPlaying, @GameType, @Visibility, @Rounds, @PlaylistCode)
            RETURNING Id
            """, room);
    }

    public async Task<IList<RoomInfo>> FindRoomsForUserAsync(int userId)
    {
        var query = await DB.QueryAsync<RoomInfo>(
            """
            select r.* from rooms r
            where r.visibility_type = @Public
            """, new { Public = (int) Visibility.Public });
        return query.ToList();
    }

    public async Task AddPlayerAsync(int roomId, int userId)
    {
        await DB.ExecuteAsync(
            """
            insert into room_players (room_id, user_id)
            values (@roomId, @userId)
            """, new {roomId, userId});
    }

    public Task<RoomInfo> GetByIdAsync(int roomId)
        => DB.QuerySingleOrDefaultAsync<RoomInfo>("select * from rooms where id = @roomId", new {roomId});
}