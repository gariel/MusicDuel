using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Messaging;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly IUserInfo _userInfo;
    private readonly IMessageBroker _broker;

    public GamesController(IUserInfo userInfo, IMessageBroker broker)
    {
        _userInfo = userInfo;
        _broker = broker;
    }

    [HttpPost("{roomId}")]
    public async Task Pooling([FromRoute] string roomId, CancellationToken cancellationToken)
    {
        var events = new List<string>(); 
        _broker.Subscribe(new UserRoomSubKey(_userInfo.Email, roomId), events.Add);
        
        Response.ContentType = "text/plain";
        StreamWriter sw;
        await using ((sw = new StreamWriter(Response.Body)).ConfigureAwait(false))
        {
            var lastId = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var count = events.Count;
                while (lastId < count)
                {
                    await sw.WriteLineAsync(events[lastId]).ConfigureAwait(false);
                    await sw.FlushAsync().ConfigureAwait(false);
                    lastId++;
                }
            }
        }
    }

    [HttpPut("{roomId}/{message}")]
    public void Teste([FromRoute] string roomId, [FromRoute] string message)
    {
        _broker.Publish(new UserRoomSubKey(_userInfo.Email, roomId), message);
    }
}

/*
browser1 -Login-> server/login
browser1 <-token- server/login
browser1 -new_room-> server/room
browser1 <-id- server/room
browser1 -pooling-> server/game

browser2 -Login-> server/login
browser2 <-token- server/login
browser2 -join id-> server/room
browser2 <-result- server/room
browser2 -pooling-> server/game

browser2 <-pooling info- server/game
browser1 <-pooling info- server/game

---------

join ->
participants <-
start game <-
music & options <-
choose & time ->
game result <-
*/
