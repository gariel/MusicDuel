namespace Domain;

public class Playlist
{
    public string Code { get; init; }
    public Uri Uri => new Uri($"https://open.spotify.com/playlist/{Code}");
    public string SpotifyKey => $"spotify:playlist:{Code}";
}