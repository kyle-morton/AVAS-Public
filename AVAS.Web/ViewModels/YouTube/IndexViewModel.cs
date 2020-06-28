using AVAS.Core.Domain;
using AVAS.Core.Models.YouTube;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace AVAS.Web.ViewModels.YouTube
{
    public class IndexViewModel
    {
        public string SearchTerm { get; set; }
        public VideoRequestType Type { get; set; }
        public List<VideoViewModel> VideoResults { get; set; }

        public static IndexViewModel From()
        {
            return new IndexViewModel
            {
                VideoResults = new List<VideoViewModel>(),
                Type = VideoRequestType.SingleVideo
            };
        }

        public static IndexViewModel From(string searchTerm, VideoRequestType type, List<SearchResult> results)
        {
            return new IndexViewModel
            {
                SearchTerm = searchTerm,
                Type = type,
                VideoResults = results.Select(VideoViewModel.From).ToList()
            };
        }
    }
}
