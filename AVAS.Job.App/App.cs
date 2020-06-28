using AVAS.Job.Core.Model;
using AVAS.Job.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AVAS.Job.App
{

    public class App
    {

        private IConfiguration _config;
        private IVideoQueueService _videoService;
        private IDownloadService _youtubeService;

        public App(IConfiguration config, IVideoQueueService videoService, IDownloadService youtubeService)
        {
            _config = config;
            _videoService = videoService;
            _youtubeService = youtubeService;
        }

        public async Task Run()
        {
            while(true)
            {
                try
                {
                    Console.WriteLine("Polling...");
                    var request = await _videoService.GetNextRequestAsync();
                    if (request != null && request.Id > 0)
                    {
                        Console.WriteLine("Request found: " + request.Id);

                        try
                        {
                            // process w/ youtube-dl
                            _youtubeService.ProcessVideoRequest(request);

                            // update to completed status
                            request.Status = VideoRequestStatus.Completed;
                            await _videoService.UpdateRequestStatusAsync(request);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.StackTrace);

                            request.Status = VideoRequestStatus.Error;
                            await _videoService.UpdateRequestStatusAsync(request);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No requests found...");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + ex.StackTrace);
                }

                // sleep for 60 seconds, then re-poll
                Thread.Sleep(60000); 
            }
            // poll for new messages
        }
    }

}
