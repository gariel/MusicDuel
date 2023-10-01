using Application.Common.Interfaces;

namespace WebApi.Security;

public class AuthenticationInfoProvider : IAuthenticationInfoProvider
{
    private readonly IEnvProvider _envProvider;

    public AuthenticationInfoProvider(IEnvProvider envProvider)
    {
        _envProvider = envProvider;
    }

    public string AuthenticationKey => _envProvider.StringValue("AUTHENTICATION_KEY");
    public string AuthenticationIssuer => _envProvider.StringValue("AUTHENTICATION_ISSUER");
    public string AuthenticationAudience => _envProvider.StringValue("AUTHENTICATION_AUDIENCE");
    public long AuthenticationExpirationSeconds => _envProvider.LongValue("AUTHENTICATION_EXPIRATION_SECONDS", 60 * 60);
}