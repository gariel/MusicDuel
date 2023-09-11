using Application.Common;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Game.Rooms;

public class CreateRoomRequest : IRequest<RoomInfo>
{
    public class CreateRoomRequestHandler : IRequestHandler<CreateRoomRequest, RoomInfo>
    {
        private readonly IUserInfo _userInfo;
        private readonly IRooms _rooms;

        public CreateRoomRequestHandler(IUserInfo userInfo, IRooms rooms)
        {
            _userInfo = userInfo;
            _rooms = rooms;
        }

        public async Task<RoomInfo> Handle(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            // if (_rooms.Values.Any(r => r!.Players.Any(p => p.Name == _userInfo.Name)))
            //     throw new Exception("user already in a room");

            var room = new RoomInfo();
            await _rooms.InsertAsync(room);
            return room;
        }
    }
}