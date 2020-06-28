using AVAS.Core.Domain;
using Humanizer;
using System;

namespace AVAS.Web.ViewModels.Videos
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public VideoRequestStatus Status { get; set; }
        public string StatusHumanized => Status.Humanize();
        public VideoRequestType Type { get; set; }
        public string TypeHumanized => Type.Humanize();
        public DateTime CreateDate { get; set; }
        public string CreateDateHumanized => CreateDate.ToString("MM/dd/yyyy hh:mm");
        public DateTime ModifyDate { get; set; }
        public string ModifyDateHumanized => ModifyDate.ToString("MM/dd/yyyy hh:mm");
        public string OutputLocation { get; set; }
        public string ContentId { get; set; }


        public static RequestViewModel From(VideoRequest request)
        {
            return new RequestViewModel
            {
                Id = request.Id,
                ContentId = request.ContentID,
                CreateDate = request.CreateDate,
                ModifyDate = request.ModifyDate,
                OutputLocation = request.OutputLocation,
                Status = request.Status,
                Type = request.Type
            };
        }

    }
}
