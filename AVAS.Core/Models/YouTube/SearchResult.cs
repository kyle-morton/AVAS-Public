using AVAS.Core.Clients.Models;
using AVAS.Core.Domain;

namespace AVAS.Core.Models.YouTube
{
    public class SearchResult
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
        public static SearchResult From(YouTubeSearchResult result)
        {
            return new SearchResult
            {
                Type = result.Type,
                Author = result.Author,
                Description = result.Description,
                Duration = result.Duration,
                Thumbnail = result.Thumbnail,
                Title = result.Title,
                Url = result.Url,
                ViewCount = result.ViewCount,
                VideoCount = result.VideoCount
            };
        }
    }
}
