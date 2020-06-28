using AVAS.Core.Domain;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVAS.Web.Helpers
{
    public static class VideoRequestHelper
    {
        public static List<SelectListItem> GetOutputLocationItems()
        {
            // todo: add archive locations here
            var locations = new List<string>
            {
                @"C:\YoutubeArchive\"
            };

            return locations.Select(l => new SelectListItem(l, l)).ToList();
        }

        public static List<SelectListItem> GetRequestTypeItems()
        {
            var types = Enum.GetValues(typeof(VideoRequestType)).Cast<VideoRequestType>();

            return types.Select(t => new SelectListItem(t.Humanize(), ((int)t).ToString())).ToList();
        }
    }
}
