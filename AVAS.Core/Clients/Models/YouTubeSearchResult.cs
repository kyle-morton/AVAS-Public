using AVAS.Core.Domain;
using YouTubeSearch;

namespace AVAS.Core.Clients.Models
{
    public class YouTubeSearchResult
    {
        public VideoRequestType Type { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ViewCount { get; set; }
        public string VideoCount { get; set; }

        public static YouTubeSearchResult From(PlaylistSearchComponents component)
        {
            return new YouTubeSearchResult
            {
                Author = component.getAuthor(),
                Thumbnail = component.getThumbnail(),
                Title = component.getTitle(),
                Url = component.getUrl(),
                VideoCount = component.getVideoCount(),
                Type = VideoRequestType.Playlist
            };
        }

        public static YouTubeSearchResult From(ChannelSearchComponents component)
        {
            return new YouTubeSearchResult
            {
                Description = component.getDescription(),
                Thumbnail = component.getThumbnail(),
                Title = component.getTitle(),
                Url = component.getUrl(),
                VideoCount = component.getVideoCount(),
                Type = VideoRequestType.Channel
            };
        }

        public static YouTubeSearchResult From(VideoSearchComponents component)
        {
            return new YouTubeSearchResult
            {
                Author = component.getAuthor(),
                Description = component.getDescription(),
                Duration = component.getDuration(),
                Thumbnail = component.getThumbnail(),
                Title = component.getTitle(),
                Url = component.getUrl(),
                ViewCount = component.getViewCount(),
                Type = VideoRequestType.SingleVideo
            };
        }
    }
}
