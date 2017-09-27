#region BuienRadarAPI - MIT - (c) 2017 Thijs Elenbaas.

/*
  DS Photosorter - tool that processes photos captured with Synology DS Photo

  Permission is hereby granted, free of charge, to any person obtaining
  a copy of this software and associated documentation files (the
  "Software"), to deal in the Software without restriction, including
  without limitation the rights to use, copy, modify, merge, publish,
  distribute, sublicense, and/or sell copies of the Software, and to
  permit persons to whom the Software is furnished to do so, subject to
  the following conditions:

  The above copyright notice and this permission notice shall be
  included in all copies or substantial portions of the Software.

  Copyright 2017 - Thijs Elenbaas
*/

#endregion

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