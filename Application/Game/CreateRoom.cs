using Application.Common;
using Domain;
using MediatR;

namespace Application.Game;

public class CreateRoomRequest : IRequest<RoomInfo>
{
    // none

    public class CreateRoomRequestHandler : IRequestHandler<CreateRoomRequest, RoomInfo>
    {
        private readonly IUserInfo _userInfo;

        public CreateRoomRequestHandler(IUserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public async Task<RoomInfo> Handle(CreateRoomRequest request, CancellationToken cancellationToken)
        {

            return new RoomInfo();
        }
    }
}