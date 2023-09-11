namespace Application.Common;

public interface IUserInfo
{
    string Name { get; set; }
}

public class UserInfo : IUserInfo
{
    public string Name { get; set; }
}