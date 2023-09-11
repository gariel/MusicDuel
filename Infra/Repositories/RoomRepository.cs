using System.Collections.Concurrent;
using System.Linq.Expressions;
using Application.Common.Interfaces;
using Domain;

namespace Infra.Repositories;

public class RoomRepository : IRooms
{
    private static ConcurrentDictionary<string, RoomInfo> _rooms = new();
    
    public Task InsertAsync(RoomInfo room)
    {
        if (!_rooms.TryAdd(room.Id, room))
            throw new Exception("can't create room");
        
        return Task.CompletedTask;
    }

    public Task<IEnumerable<RoomInfo>> FindAsync(Expression<Func<RoomInfo, bool>> filter)
    {
        var lambda = filter.Compile();
        return Task.FromResult(_rooms.Values.Where(lambda));
    }

    public async Task AddPlayerAsync(string roomId, Player player)
    {
        var room = await GetByIdAsync(roomId);

        if (room.Players.Any(p => p.Name == player.Name))
            throw new Exception("player already joined this room");

        room.Players.Add(player);
    }

    public Task<RoomInfo> GetByIdAsync(string roomId)
    {
        if (!_rooms.TryGetValue(roomId, out var roomInfo))
            throw new Exception("can't get room by id");
        return Task.FromResult(roomInfo);
    }
}