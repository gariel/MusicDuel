using Application.Common;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Game.Rooms;

public class JoinRoomRequest : IRequest<Unit>
{
    public string RoomId { get; set; }

    public class JoinRoomRequestHandler : IRequestHandler<JoinRoomRequest, Unit>
    {
        private readonly IRooms _rooms;
        private readonly IUserInfo _userInfo;

        public JoinRoomRequestHandler(IRooms rooms, IUserInfo userInfo)
        {
            _rooms = rooms;
            _userInfo = userInfo;
        }

        public async Task<Unit> Handle(JoinRoomRequest request, CancellationToken cancellationToken)
        {
            var room = await _rooms.GetByIdAsync(request.RoomId);
            if (room.IsPlaying)
                throw new Exception("can't join a room while in game");

            var player = new Player(_userInfo.Name);
            await _rooms.AddPlayerAsync(request.RoomId, player);

            return Unit.Value;
        }
    }
}