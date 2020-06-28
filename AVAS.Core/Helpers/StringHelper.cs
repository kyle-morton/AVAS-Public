using System;
using System.Text;

namespace AVAS.Core.Helpers
{
    public static class StringHelper
    {
        public static string FromBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
