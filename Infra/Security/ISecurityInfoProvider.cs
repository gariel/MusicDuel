namespace Infra.Security;

public interface ISecurityInfoProvider
{
    string Salt { get; }
}