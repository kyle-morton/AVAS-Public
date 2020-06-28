using AVAS.Core.Clients.Models;
using AVAS.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeSearch;

namespace AVAS.Core.Clients
{
    public interface IYouTubeClient
    {
        Task<List<YouTubeSearchResult>> SearchAsync(string searchTerm, VideoRequestType type, int page);
    }
    public class YouTubeClient : IYouTubeClient
    {
        public async Task<List<YouTubeSearchResult>> SearchAsync(string searchTerm, VideoRequestType type, int page)
        {
            switch (type)
            {
                case VideoRequestType.Channel:
                    var channelResults = await new ChannelSearch().GetChannelsPaged(searchTerm, page);
                    return channelResults.Select(YouTubeSearchResult.From).ToList();
                case VideoRequestType.Playlist:
                    var playlistResults = await new PlaylistSearch().GetPlaylistsPaged(searchTerm, page);
                    return playlistResults.Select(YouTubeSearchResult.From).ToList();
                default:
                    var videoResults = await new VideoSearch().GetVideosPaged(searchTerm, page);
                    return videoResults.Select(YouTubeSearchResult.From).ToList();
            }
        }
    }
}
