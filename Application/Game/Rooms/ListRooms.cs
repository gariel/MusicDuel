using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Game.Rooms;

public class ListRoomsRequest : IRequest<IEnumerable<RoomInfo>>
{

    public class ListRoomRequestHandler : IRequestHandler<ListRoomsRequest, IEnumerable<RoomInfo>>
    {
        private readonly IRooms _rooms;

        public ListRoomRequestHandler(IRooms rooms)
        {
            _rooms = rooms;
        }

        public Task<IEnumerable<RoomInfo>> Handle(ListRoomsRequest request, CancellationToken cancellationToken)
        {
            return _rooms.FindAsync(r => r.Options.IsPublic == true);
        }
    }
}