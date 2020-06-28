using AVAS.Core.Clients;
using AVAS.Core.Domain;
using AVAS.Core.Models.YouTube;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeSearch;

namespace AVAS.Core.Services
{
    public interface IYoutubeSearchService
    {
        Task<List<SearchResult>> SearchVideosAsync(string searchTerm, VideoRequestType type, int page);
    }

    public class YoutubeService : IYoutubeSearchService
    {

        private readonly IYouTubeClient _youtubeClient;

        public YoutubeService(IYouTubeClient youtubeClient)
        {
            _youtubeClient = youtubeClient;
        }

        public async Task<List<SearchResult>> SearchVideosAsync(string searchTerm, VideoRequestType type, int page)
        {
            var videos = await _youtubeClient.SearchAsync(searchTerm, type, page);

            return videos.Select(SearchResult.From).ToList();
        }

    }
}
