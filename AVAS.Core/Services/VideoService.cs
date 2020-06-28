using AVAS.Core.Data;
using AVAS.Core.Domain;
using AVAS.Core.Extensions;
using AVAS.Core.Models.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVAS.Core.Services
{

    public interface IVideoService
    {
        Task<List<VideoRequest>> GetRequestsAsync(VideoRequestFilter filter);
        Task<VideoRequest> GetNextPendingRequestAsync();
        Task<VideoRequest> GetRequestAsync(int id);
        Task<VideoRequest> CreateRequestAsync(VideoRequest request);
        Task<bool> UpdateRequestStatusAsync(int id, VideoRequestStatus status);
    }

    public class VideoService : ServiceBase, IVideoService
    {
        public VideoService(AVASDbContext context) : base(context)
        {
        }

        public async Task<VideoRequest> CreateRequestAsync(VideoRequest request)
        {
            request.CreateDate = request.ModifyDate = DateTime.UtcNow;

            await _dbContext.AddAsync(request);
            await _dbContext.SaveChangesWithTimesAsync();

            return request;
        }

        public async Task<VideoRequest> GetRequestAsync(int id)
        {
            var request = await _dbContext.VideoRequests.SingleOrDefaultAsync(r => r.Id == id);

            return request;
        }

        public async Task<List<VideoRequest>> GetRequestsAsync(VideoRequestFilter filter)
        {
            var requests = await _dbContext.VideoRequests
                .Where(r => filter.Types.Contains(r.Type))
                .Where(r => filter.Statuses.Contains(r.Status))
                .OrderBy(r => r.Status)
                .ThenByDescending(r => r.Id)
                .ToListAsync();

            return requests;
        }

        public async Task<VideoRequest> GetNextPendingRequestAsync()
        {
            var request = await _dbContext.VideoRequests
                .Where(r => r.Status == VideoRequestStatus.Pending)
                .OrderBy(r => r.CreateDate)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                return request;
            }

            request.Status = VideoRequestStatus.Processing;
            await _dbContext.SaveChangesWithTimesAsync();

            return request;
        }

        public async Task<bool> UpdateRequestStatusAsync(int id, VideoRequestStatus status)
        {
            var request = await _dbContext.VideoRequests.SingleOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                return false;
            }

            request.Status = status;
            await _dbContext.SaveChangesWithTimesAsync();

            return true;
        }
    }
}
