using Application.Game.Rooms;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IList<RoomInfo>> ListPublicRooms()
    {
        var rooms = await _mediator.Send(new ListRoomsRequest());
        return rooms.ToList();
    }

    [HttpGet("{roomId:int}")]
    public Task<RoomInfo> GetRoomInfo([FromRoute] int roomId)
    {
        return _mediator.Send(new GetRoomInfoRequest
        {
            RoomId = roomId,
        });
    }
    
    [HttpPost]
    public Task<RoomInfo> NewRoom()
    {
        return _mediator.Send(new CreateRoomRequest());
    }
    
    [HttpPost("{roomId:int}/join")]
    public async Task JoinRoom([FromRoute] int roomId)
    {
        await _mediator.Send(new JoinRoomRequest
        {
            RoomId = roomId,
        });
    }
}