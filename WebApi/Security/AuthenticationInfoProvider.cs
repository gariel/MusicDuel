using Application.Common.Interfaces;

namespace WebApi.Security;

public class AuthenticationInfoProvider : IAuthenticationInfoProvider
{
    private readonly IInfoProvider _infoProvider;

    public AuthenticationInfoProvider(IInfoProvider infoProvider)
    {
        _infoProvider = infoProvider;
    }

    public string AuthenticationKey => _infoProvider.StringValue("AUTHENTICATION_KEY");
    public string AuthenticationIssuer => _infoProvider.StringValue("AUTHENTICATION_ISSUER");
    public string AuthenticationAudience => _infoProvider.StringValue("AUTHENTICATION_AUDIENCE");
    public long AuthenticationExpirationSeconds => _infoProvider.LongValue("AUTHENTICATION_EXPIRATION_SECONDS", 60 * 60);
}