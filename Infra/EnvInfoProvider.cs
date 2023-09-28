using Application.Common.Interfaces;

namespace Infra;

public class EnvInfoProvider : IInfoProvider
{
    public string StringValue(string key, string @default="")
        => Environment.GetEnvironmentVariable(key) ?? @default;

    public long LongValue(string key, long @default = 0)
    {
        var value = Environment.GetEnvironmentVariable(key);
        
        if (value is null)
            return @default;
        
        return Convert.ToInt64(value);
    }
}