namespace WebApi.Security;

public interface IAuthenticationInfoProvider
{
    string AuthenticationKey { get; }
    string AuthenticationIssuer { get; }
    string AuthenticationAudience { get; }
    long AuthenticationExpirationSeconds { get; }
}