using AVAS.Core.Domain;
using AVAS.Core.Models.YouTube;

namespace AVAS.Web.ViewModels.YouTube
{
    public class VideoViewModel
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
        public string ViewOrVideoHumanized
        {
            get
            {
                if (Type == VideoRequestType.SingleVideo)
                {
                    return ViewCount + "K Views";
                }

                return VideoCount + " Videos";
            }
        }


        public static VideoViewModel From(SearchResult result)
        {

            return new VideoViewModel
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
