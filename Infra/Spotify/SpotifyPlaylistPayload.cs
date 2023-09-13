// ReSharper disable InconsistentNaming
namespace Infra.Spotify;

internal class SpotifyPlaylistPayload
{
    public Data data { get; set; }
    public class Data
    {
        public PlaylistV2 playlistV2 { get; set; }
        public class PlaylistV2
        {
            public Content content { get; set; }
            public class Content
            {
                public int totalCount { get; set; }
                public Item[] items { get; set; }
                public class Item
                {
                    public Itemv2 itemV2 { get; set; }
                    public class Itemv2
                    {
                        public ItemData data { get; set; }
                        public class ItemData
                        {
                            public string name { get; set; }
                            public Artists artists { get; set; }
                            public class Artists
                            {
                                public ArtistItem[] items { get; set; }
                                public class ArtistItem
                                {
                                    public Profile profile { get; set; }
                                    public class Profile
                                    {
                                        public string name { get; set; }
                                    }
                                }
                            }
                            public Previews previews { get; set; }
                            public class Previews
                            {
                                public AudioPreview audioPreviews { get; set; }
                                public class AudioPreview
                                { 
                                    public PreviewItem[] items { get; set; }
                                    public class PreviewItem
                                    {
                                        public string url { get; set; }
                                    }
                                }
                            }
                            public AlbumOfTrack albumOfTrack { get; set; }
                            public class AlbumOfTrack
                            {
                                public CoverArt coverArt { get; set; }
                                public class CoverArt
                                {
                                    public CoverSource[] sources { get; set; }
                                    public class CoverSource
                                    {
                                        public string url { get; set; }
                                        public int width { get; set; }
                                        public int height { get; set; }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}