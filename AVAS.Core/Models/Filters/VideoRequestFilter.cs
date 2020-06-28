using AVAS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVAS.Core.Models.Filters
{
    public class VideoRequestFilter
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public List<VideoRequestStatus> Statuses { get; set; }
        public List<VideoRequestType> Types { get; set; }

        public VideoRequestFilter()
        {
            Page = 1;
            Size = 10;
            Statuses = new List<VideoRequestStatus> { VideoRequestStatus.Pending, VideoRequestStatus.Processing, VideoRequestStatus.Completed, VideoRequestStatus.Error, VideoRequestStatus.Removed };
            Types = new List<VideoRequestType> { VideoRequestType.Channel, VideoRequestType.Playlist, VideoRequestType.SingleVideo };
        }
    }
}
