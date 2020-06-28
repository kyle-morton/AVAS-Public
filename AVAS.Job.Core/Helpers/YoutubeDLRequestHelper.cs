using AVAS.Job.Core.Clients.Models.YoutubeDL;
using AVAS.Job.Core.Model;
using System.Collections.Generic;

namespace AVAS.Job.Core.Helpers
{
    public static class YoutubeDLRequestHelper
    {
        public static string BuildArguments(YoutubeDLRequest request)
        {
            var argsList = new List<string>();

            argsList = BuildGenericArguments(request, argsList);

            // add continue on error if not a single video
            if (request.Type != VideoRequestType.SingleVideo)
            {
                argsList.Add($"-i");
            }

            argsList.Add($"{request.ContentID}");

            return string.Join(' ', argsList);
        }

        private static List<string> BuildGenericArguments(YoutubeDLRequest request, List<string> argsList)
        {
            // output location
            var outputLocation = request.OutputLocation + "%(title)s.%(ext)s";
            argsList.Add($"-o {outputLocation}");

            if (request.AudioOnly) // get MP3 if audio-only
            {
                argsList.Add("--extract-audio --audio-format mp3");
            }
            else // otherwise -> mp4
            {
                argsList.Add("-f mp4");
            }

            return argsList;
        }
    }
}
