using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Game;

public class GameQuestionRequest  : IRequest<GameQuestion>
{
    private static readonly HttpClient Client = new();
    
    public GameType Type { get; set; }
    public Playlist Playlist { get; set; }

    public class GameQuestionRequestHandler : IRequestHandler<GameQuestionRequest, GameQuestion>
    {
        private readonly ISpotifyMusic _spotify;

        public GameQuestionRequestHandler(ISpotifyMusic spotify)
        {
            _spotify = spotify;
        }

        public async Task<GameQuestion> Handle(GameQuestionRequest request, CancellationToken cancellationToken)
        {
            var musics = await _spotify.MusicsFromPlaylist(request.Playlist)
                .ToListAsync(cancellationToken);

            var artists = musics.Select(m => m.Artist)
                .Distinct()
                .ToList();

            var gameRealType = request.Type;
            if (request.Type == GameType.MusicOrArtits)
            {
                gameRealType = new[]
                {
                    GameType.Music,
                    GameType.Artist,
                }[Random.Shared.Next(1)];
            }

            Music[] choosen;
            Func<Music, string> getTitle;
            if (gameRealType == GameType.Artist)
            {
                var indexes = GetRandomIndexes(4, artists.Count - 1);
                choosen = indexes.Select(i =>
                {
                    var artistMusics = musics
                        .Where(m => m.Artist == artists[i])
                        .ToArray();
                    return artistMusics[Random.Shared.Next(artistMusics.Length - 1)];
                }).ToArray();
                getTitle = m => m.Artist;
            }
            else
            {
                var indexes = GetRandomIndexes(4, musics.Count - 1);
                choosen = indexes
                    .Select(i => musics[i])
                    .ToArray();

                if (gameRealType == GameType.MusicWithArtits)
                    getTitle = m => $"{m.Artist} - {m.Name}";
                else
                    getTitle = m => m.Name;
            }

            var correct = Random.Shared.Next(3);
            var items = await Task.WhenAll(choosen.Select(async (c, i) => new GameQuestion.GameQuestionItem
            {
                ImageBase64 = await DownloadBase64(c.CoverLink),
                Title = getTitle(c),
                Correct = i == correct,
            }));
            
            return new GameQuestion
            {
                GameType = gameRealType,
                Mp3Base64 = await DownloadBase64(choosen[correct].PreviewLink),
                Items = items,
            };
        }

        private static async Task<string> DownloadBase64(Uri uri)
        {
            var bytes = await Client.GetByteArrayAsync(uri);
            return Convert.ToBase64String(bytes);
        }

        private int[] GetRandomIndexes(int size, int max)
        {
            var indexes = new List<int>();
            while (indexes.Count < size)
            {
                var newIndex = Random.Shared.Next(max);
                if (!indexes.Contains(newIndex))
                    indexes.Add(newIndex);
            }

            return indexes.ToArray();
        }
    }
}