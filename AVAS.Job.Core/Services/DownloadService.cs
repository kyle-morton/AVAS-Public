using AVAS.Job.Core.Clients;
using AVAS.Job.Core.Clients.Models.YoutubeDL;
using AVAS.Job.Core.Model;

namespace AVAS.Job.Core.Services
{

    public interface IDownloadService
    {
        void ProcessVideoRequest(VideoRequest request);
    }

    public class DownloadService : IDownloadService
    {
        private readonly IYoutubeDLClient _youtubeDLClient;

        public DownloadService(IYoutubeDLClient youtubeDLClient)
        {
            _youtubeDLClient = youtubeDLClient;
        }

        public void ProcessVideoRequest(VideoRequest request)
        {
            var youtubeDLReq = YoutubeDLRequest.From(request);

            _youtubeDLClient.DownloadRequestedContent(youtubeDLReq);
        }

    }
}
