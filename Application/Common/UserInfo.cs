namespace Application.Common;

public interface IUserInfo
{
    int Id { get; set; }
    string Name { get; set; }
}

public class UserInfo : IUserInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
}