using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Game.Rooms;

public class GetRoomInfoRequest : IRequest<RoomInfo>
{
    public int RoomId { get; set; }

    public class GetRoomInfoRequestHandler : IRequestHandler<GetRoomInfoRequest, RoomInfo>
    {
        private readonly IRooms _rooms;

        public GetRoomInfoRequestHandler(IRooms rooms)
        {
            _rooms = rooms;
        }

        public Task<RoomInfo> Handle(GetRoomInfoRequest request, CancellationToken cancellationToken)
        {
            return _rooms.GetByIdAsync(request.RoomId);
        }
    }
}