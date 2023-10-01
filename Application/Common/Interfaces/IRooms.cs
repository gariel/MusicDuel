using Domain;

namespace Application.Common.Interfaces;

public interface IRooms
{
    public Task<int> InsertAsync(RoomInfo room);
    Task<IList<RoomInfo>> FindRoomsForUserAsync(int userId);
    Task<RoomInfo> GetByIdAsync(int roomId);
    Task AddPlayerAsync(int roomId, int player);
}