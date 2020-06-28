using AVAS.Job.Core.Clients.Models.YoutubeDL;
using AVAS.Job.Core.Helpers;
using System;
using System.Diagnostics;

namespace AVAS.Job.Core.Clients
{
    public interface IYoutubeDLClient
    {
        void DownloadRequestedContent(YoutubeDLRequest request);
    }
    public class YoutubeDLClient : IYoutubeDLClient
    {
        public void DownloadRequestedContent(YoutubeDLRequest request)
        {
            var args = YoutubeDLRequestHelper.BuildArguments(request);
            RunYoutubeDLProcess(args);
        }

        private void RunYoutubeDLProcess(string arguments)
        {
            var processInfo = new ProcessStartInfo(@"YoutubeDL\youtube-dl.exe", arguments);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                Console.WriteLine("output>>" + e.Data);
            };
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                Console.WriteLine("error>>" + e.Data);
            };
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }
    }
}
