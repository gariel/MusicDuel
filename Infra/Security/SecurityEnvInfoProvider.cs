using Application.Common.Interfaces;

namespace Infra.Security;

public class SecurityEnvInfoProvider : ISecurityInfoProvider
{
    private readonly IEnvProvider _envProvider;

    public SecurityEnvInfoProvider(IEnvProvider envProvider)
    {
        _envProvider = envProvider;
    }

    public string Salt => _envProvider.StringValue("SECURITY_SALT_VALUE");
}