using System.Linq.Expressions;
using Domain;

namespace Application.Common.Interfaces;

public interface IRooms
{
    public Task InsertAsync(RoomInfo room);
    Task<IEnumerable<RoomInfo>> FindAsync(Expression<Func<RoomInfo, bool>> filter);
    Task<RoomInfo> GetByIdAsync(string roomId);
    Task AddPlayerAsync(string roomId, Player player);
}