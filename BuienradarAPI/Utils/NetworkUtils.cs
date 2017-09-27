using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Xml;

namespace Buienradar.Utils
{
    public static class NetworkUtils
    {
        public static bool Connected()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) return false;

            return NetworkInterface.GetIsNetworkAvailable();
        }

        public static bool WaitForConnection(TimeSpan timeout)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            // Do network related stuff
            while (!Connected() && stopWatch.Elapsed < timeout - TimeSpan.FromSeconds(30))
                Thread.Sleep(1000);
            return Connected();
        }

        public static XmlReader XmlTextReader(string url, TimeSpan timeout)
        {
            return XmlTextReader(url, (int) timeout.TotalMilliseconds);
        }

        public static XmlReader XmlTextReader(string url, int timeout)
        {
            var request = WebRequest.Create(url);
            request.Timeout = timeout;
            using (var response = request.GetResponse())
            {
                var stream = response.GetResponseStream();
                var reader = stream == null ? null : XmlReader.Create(stream);
                return reader;
            }
        }
    }
}