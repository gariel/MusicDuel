namespace Application.Common.Interfaces;

public interface IHashService
{
    string Hash(string content, string saltSeed);
}