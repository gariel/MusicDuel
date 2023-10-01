namespace Domain;

public class RoomInfo
{
    public int Id { get; set; }
    public string Title { get; set; } = Generators.NewRandomTitle();
    public bool IsPlaying { get; set; }
    public Visibility Visibility { get; set; }
    public GameType GameType { get; set; } = GameType.MusicOrArtits;
    public int MaxPlayers { get; set; } = 10;
    public int Rounds { get; set; } = 10;
    public PlaylistInformation Playlist { get; set; }
    public List<string> Players { get; } = new();
    
    public string? Host => Players.FirstOrDefault();
    public string PlaylistCode => Playlist?.Code ?? "";
}