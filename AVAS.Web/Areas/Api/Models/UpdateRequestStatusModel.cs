using AVAS.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace AVAS.Web.Areas.Api.Models
{
    public class UpdateRequestStatusModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public VideoRequestStatus Status { get; set; }
    }
}
