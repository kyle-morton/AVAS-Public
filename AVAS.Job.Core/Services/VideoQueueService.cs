using AVAS.Job.Core.Model;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AVAS.Job.Core.Services
{

    public interface IVideoQueueService
    {
        Task<VideoRequest> GetNextRequestAsync();
        Task UpdateRequestStatusAsync(VideoRequest req);
    }

    public class VideoQueueService : IVideoQueueService
    {

        private readonly string _apiUrl;

        public VideoQueueService(IConfiguration config)
        {
            _apiUrl = config["Azure:ApiUrl"];
        }

        public async Task<VideoRequest> GetNextRequestAsync()
        {
            VideoRequest req = null;
            using (var client = new HttpClient())
            {
                var json = JsonSerializer.Serialize(new { });
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl + "pop", stringContent);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                req = JsonSerializer.Deserialize<VideoRequest>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return req;
        }

        public async Task UpdateRequestStatusAsync(VideoRequest req)
        {
            using (var client = new HttpClient())
            {
                var json = JsonSerializer.Serialize(req);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl + "updatestatus", stringContent);

                response.EnsureSuccessStatusCode();
            }

        }
    }
}
