namespace Domain;

public class Room
{
    public Room(bool isIsPublic)
    {
        IsPublic = isIsPublic;
    }

    public string Id { get; } = Guid.NewGuid().ToString();
    public bool IsPublic { get; set; }
    public List<string> Players { get; set; } = new();
    public bool IsPlaying { get; set; }
}