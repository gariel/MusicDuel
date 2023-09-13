using Domain;

namespace Application.Common.Interfaces;

public interface ISpotifyMusic
{
    public IAsyncEnumerable<Music> MusicsFromPlaylist(Playlist playlist);
}