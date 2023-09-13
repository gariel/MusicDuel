namespace Domain;

public class GameOptions
{
    public GameType GameType { get; set; } = GameType.MusicOrArtits;
    public bool IsPublic { get; set; } = false;
    public int MaxPlayers { get; set; } = 10;
    public int Rounds { get; set; } = 10;
    
    public PlaylistInformation PlaylistInformation { get; set; }
}