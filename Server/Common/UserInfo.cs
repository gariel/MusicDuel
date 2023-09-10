namespace Server.Common;

public interface IUserInfo
{
    string Email { get; set; }
}

public class UserInfo : IUserInfo
{
    public string Email { get; set; }
}