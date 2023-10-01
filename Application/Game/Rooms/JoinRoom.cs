using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Game.Rooms;

public class JoinRoomRequest : IRequest<Unit>
{
    public int RoomId { get; set; }

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

            await _rooms.AddPlayerAsync(request.RoomId, _userInfo.Id);

            return Unit.Value;
        }
    }
}