using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Application.Common.Interfaces;
using Domain;

namespace Infra.Spotify;

public partial class SpotifyMusicResolver : ISpotifyMusic
{
    const string UserAgent = 
        "Mozilla/5.0 (Macintosh;" +
        " Intel Mac OS X 10.15; rv:109.0)" +
        " Gecko/20100101" + 
        " Firefox/117.0";

    private const string ExtensionQs = 
        "{\"persistedQuery\":" +
        "{\"version\":1," +
        "\"sha256Hash\":\"9a264b807ded66cd2baea5d3bc2349b44f370176840fefd861b9c727b2a289f8\"}}";
    
    private const string SpotifyQueryUrl = "https://api-partner.spotify.com/pathfinder/v1/query";

    [GeneratedRegex("\\<script\\sid=\"session\".*>\\{\"accessToken\":\"([\\w-]+)\",")]
    private static partial Regex AccessTokenRe();
    
    private readonly ISerializer _serializer;
    private readonly HttpClient _httpClient;

    public SpotifyMusicResolver(ISerializer serializer)
    {
        _serializer = serializer;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
    }

    public async IAsyncEnumerable<Music> MusicsFromPlaylist(Playlist playlist)
    {
        var content = await _httpClient.GetStringAsync(playlist.Uri);
        
        var token = ExtractToken(content);
        if (token is not null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var offset = 0;
        int size;
        const int step = 30;
        do
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("operationName", "queryPlaylist");
            queryString.Add("variables", $"{{\"uri\":\"{playlist.SpotifyKey}\",\"limit\":{step},\"offset\":{offset}}}");
            queryString.Add("extensions", ExtensionQs);
            
            var builder = new UriBuilder(SpotifyQueryUrl)
            {
                Query = queryString.ToString()
            };

            var data = await _httpClient.GetStringAsync(builder.Uri);
            var playlistData = _serializer.Deserialize<SpotifyPlaylistPayload>(data);

            size = playlistData!.data.playlistV2.content.totalCount;

            foreach (var item in playlistData!.data.playlistV2.content.items)
            {
                var artist = item.itemV2.data.artists.items.First().profile.name;
                var title = item.itemV2.data.name;
                var mp3Link = item.itemV2.data.previews.audioPreviews.items.FirstOrDefault()?.url;
                var coverLink = item.itemV2.data.albumOfTrack.coverArt.sources.MaxBy(s => s.width)!.url;
                if (mp3Link is not null)
                {
                    yield return new Music
                    {
                        Name = title,
                        Artist = artist,
                        CoverLink = new Uri(coverLink),
                        PreviewLink = new Uri(mp3Link),
                    };
                }
            }
            offset += step;
        } while (offset < size);

    }

    private string? ExtractToken(string content)
    {
        var match = AccessTokenRe().Match(content);
        if (match.Success)
            return match.Groups[1].ToString();
        return null;
    }
}