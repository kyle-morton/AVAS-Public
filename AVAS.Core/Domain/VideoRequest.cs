namespace AVAS.Core.Domain
{

    public class VideoRequest : EntityBase
    {
        public VideoRequestType Type { get; set; }
        public VideoRequestStatus Status { get; set; }
        public bool AudioOnly { get; set; }
        public string OutputLocation { get; set; }
        public string ContentID { get; set; }
    }
}
