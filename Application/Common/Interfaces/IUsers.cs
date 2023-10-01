namespace Application.Common.Interfaces;

public interface IUsers
{
    Task<int?> ValidateUserAuthentication(string userName, string password);
    Task CreateUser(string userName, string password);
}