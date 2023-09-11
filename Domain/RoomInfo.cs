namespace Domain;

public class RoomInfo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public bool IsPlaying { get; set; }
    public GameOptions Options { get; } = new();
    public List<Player> Players { get; } = new();
    public string? Host => Players.FirstOrDefault()?.Name;
}