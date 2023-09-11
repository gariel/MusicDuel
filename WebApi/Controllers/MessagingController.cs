using Application.Common;
using Application.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagingController : ControllerBase
{
    private readonly IUserInfo _userInfo;
    private readonly IMessageBroker _broker;

    public MessagingController(IUserInfo userInfo, IMessageBroker broker)
    {
        _userInfo = userInfo;
        _broker = broker;
    }

    [HttpPost("/rooms/{roomId}")]
    public async Task RoomEndpoint([FromRoute] string roomId, CancellationToken cancellationToken)
    {
        var events = new List<string>();
        var key = new UserRoomSubKey(_userInfo.Name, roomId);
        _broker.Subscribe(key, events.Add);
        
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

        _broker.Unsubscribe(key);
    }
}
