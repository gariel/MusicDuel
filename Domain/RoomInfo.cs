namespace Domain;

public record struct RoomInfo
{
    public string Id { get; init; }
    public bool IsPublic { get; init; }
    public List<string> Players { get; init; }
    public string? Host { get; init; }
    public bool IsPlaying { get; set; }
    
    public static explicit operator RoomInfo(Room room) => new()
    {
        Host = room.Players.FirstOrDefault(),
        Players = room.Players.ToList(),
        Id = room.Id,
        IsPublic = room.IsPublic,
        IsPlaying = room.IsPlaying,
    };
}