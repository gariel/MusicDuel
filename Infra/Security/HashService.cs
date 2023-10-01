using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces;

namespace Infra.Security;

public class HashService : IHashService, IDisposable
{
    private readonly ISecurityInfoProvider _secInfoProvider;
    private readonly SHA1 _sha1;

    public HashService(ISecurityInfoProvider secInfoProvider)
    {
        _secInfoProvider = secInfoProvider;
        _sha1 = SHA1.Create();
    }

    public string Hash(string content, string saltSeed)
    {
        var sContent = Hash(content);
        var sSaltSeed = Hash(_secInfoProvider.Salt + saltSeed);
        var sHashed = Hash(sContent + sSaltSeed);
        return sHashed;
    }

    private string Hash(string content)
        => Convert.ToHexString(_sha1.ComputeHash(Encoding.UTF8.GetBytes(content)));

    public void Dispose()
    {
        _sha1.Dispose();
    }
}