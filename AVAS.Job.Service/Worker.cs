using AVAS.Job.Core.Model;
using AVAS.Job.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AVAS.Job.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConfiguration _config;
        private IVideoQueueService _videoService;
        private IDownloadService _youtubeService;

        public Worker(ILogger<Worker> logger, IConfiguration config, IVideoQueueService videoService, IDownloadService youtubeService)
        {
            _config = config;
            _videoService = videoService;
            _youtubeService = youtubeService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AVAS Running...");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var request = await _videoService.GetNextRequestAsync();
                    if (request != null && request.Id > 0)
                    {
                        _logger.LogWarning("Request found: " + request.Id);

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
                            _logger.LogError(ex, ex.Message);

                            request.Status = VideoRequestStatus.Error;
                            await _videoService.UpdateRequestStatusAsync(request);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("No requests found...");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }

                // sleep for 60 seconds, then re-poll
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
