using System.ComponentModel;

namespace Domain;

public enum GameType
{
    [Description("Only Music Names")]
    Music = 1,
    
    [Description("Only Artists Names")]
    Artist = 2,
    
    [Description("Musics and Artists (Classic)")]
    MusicOrArtits = 3,
    
    [Description("Music and it's Artists together")]
    MusicWithArtits = 4,
}