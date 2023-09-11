namespace Application.Common.Messaging;

public interface ISubscriptionKey
{
    string Parse();
}

public class UserRoomSubKey : ISubscriptionKey
{
    private readonly string _email;
    private readonly string _roomId;

    public UserRoomSubKey(string email, string roomId)
    {
        _email = email;
        _roomId = roomId;
    }

    public string Parse() => $"{_email}|{_roomId}";
}