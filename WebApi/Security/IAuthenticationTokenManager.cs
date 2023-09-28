namespace WebApi.Security;

public interface IAuthenticationTokenManager
{
    string BuildToken(TokenInfo info);
    TokenInfo ValidateToken(string token);
}