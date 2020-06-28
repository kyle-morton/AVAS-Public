using AVAS.Core.Domain;
using AVAS.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AVAS.Web.ViewModels.Videos
{
    public class CreateRequestViewModel
    {
        [Required]
        public VideoRequestType Type { get; set; }
        [Required]
        public bool AudioOnly { get; set; }
        [Required]
        public string OutputLocation { get; set; }
        [Required]
        public string FolderName { get; set; }
        [Required]
        public string ContentID { get; set; }

        public static CreateRequestViewModel From()
        {
            return new CreateRequestViewModel
            {
                AudioOnly = false,
                OutputLocation = VideoRequestHelper.GetOutputLocationItems().First().Value,
                Type = VideoRequestType.SingleVideo,
                ContentID = "",
                FolderName = ""
            };
        }

        public static CreateRequestViewModel From(string url)
        {
            return new CreateRequestViewModel
            {
                AudioOnly = false,
                OutputLocation = VideoRequestHelper.GetOutputLocationItems().First().Value,
                Type = VideoRequestType.SingleVideo,
                ContentID = url,
                FolderName = ""
            };
        }

        public VideoRequest ToModel()
        {
            return new VideoRequest
            {
                AudioOnly = AudioOnly,
                OutputLocation = @$"{OutputLocation}{FolderName.Trim()}\",
                Type = Type,
                ContentID = ContentID,
                Status = VideoRequestStatus.Pending
            };
        }
    }
}
