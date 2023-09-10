using System.Collections.Concurrent;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Server.Common;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IUserInfo _userInfo;
    private static ConcurrentDictionary<string, Room?> _rooms = new();

    public RoomsController(IUserInfo userInfo)
    {
        _userInfo = userInfo;
    }

    [HttpGet]
    public IList<RoomInfo> ListPublicRooms()
    {
        return _rooms.Values
            .Where(r => r!.IsPublic)
            .Select(r => (RoomInfo) r!)
            .ToList();
    }

    [HttpGet("{roomId}")]
    public RoomInfo GetRoomInfo([FromRoute] string roomId)
    {
        var room = FindRoom(roomId);
        return (RoomInfo)room;
    }
    
    [HttpPost]
    public RoomInfo NewRoom([FromBody] bool isPublic)
    {
        if (_rooms.Values.Any(r => r!.Players.Any(p => p == _userInfo.Email)))
            throw new Exception("user already in a room");
        
        var room = new Room(isPublic);
        if (!_rooms.TryAdd(room.Id, room))
            throw new Exception("can't create room");

        JoinRoom(room.Id);
        return GetRoomInfo(room.Id);
    }
    
    [HttpPost("{roomId}/join")]
    public void JoinRoom([FromRoute] string roomId)
    {
        var room = FindRoom(roomId);
        if (room.IsPlaying)
            throw new Exception("can't join a room while in game");

        lock (room.Players)
        {   
            if (room.Players.Any(p => p == _userInfo.Email))
                throw new Exception("player already joined this room");
            room.Players.Add(_userInfo.Email);
        }
    }

    private static Room FindRoom(string roomId)
    {
        if (!_rooms.TryGetValue(roomId, out var room))
            throw new Exception("error on finding room");
        
        if (room is null)
            throw new Exception("room not found");

        return room;
    }
}