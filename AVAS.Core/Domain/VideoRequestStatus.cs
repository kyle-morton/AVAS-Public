using System.ComponentModel.DataAnnotations;

namespace AVAS.Core.Domain
{
    public enum VideoRequestStatus
    {
        [Display(Name ="Pending")]
        Pending = 0,
        [Display(Name = "Processing")]
        Processing = 1,
        [Display(Name = "Completed")]
        Completed = 2,
        [Display(Name = "Error")]
        Error = 3,
        [Display(Name = "Removed")]
        Removed = 4
    }
}
