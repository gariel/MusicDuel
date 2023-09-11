namespace Domain;

public record Playlist
{
    public string Url { get; init; }
    public string Title { get; init; }
    public string CoverUrl { get; init; }
    
    public bool HasMultipleArtits { get; init; }
}