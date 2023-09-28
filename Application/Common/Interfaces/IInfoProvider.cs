namespace Application.Common.Interfaces;

public interface IInfoProvider
{
    string StringValue(string key, string @default="");
    long LongValue(string key, long @default=0);
}