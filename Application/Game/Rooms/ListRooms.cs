using Application.Common;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Game.Rooms;

public class ListRoomsRequest : IRequest<IList<RoomInfo>>
{

    public class ListRoomRequestHandler : IRequestHandler<ListRoomsRequest, IList<RoomInfo>>
    {
        private readonly IRooms _rooms;
        private readonly IUserInfo _userInfo;

        public ListRoomRequestHandler(IRooms rooms, IUserInfo userInfo)
        {
            _rooms = rooms;
            _userInfo = userInfo;
        }

        public Task<IList<RoomInfo>> Handle(ListRoomsRequest request, CancellationToken cancellationToken)
        {
            return _rooms.FindRoomsForUserAsync(_userInfo.Id);
        }
    }
}