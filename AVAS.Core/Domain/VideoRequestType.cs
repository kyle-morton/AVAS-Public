using System.ComponentModel.DataAnnotations;

namespace AVAS.Core.Domain
{
    public enum VideoRequestType
    {
        [Display(Name = "Single Video")]
        SingleVideo,
        [Display(Name = "Playlist")]
        Playlist,
        [Display(Name = "Channel")]
        Channel
    }
}
