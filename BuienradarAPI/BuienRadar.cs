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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Buienradar.Utils;

namespace BuienRadar
{
    public class TimeRain
    {
        public DateTime Time { get; set; }
        public int RainfallIndex { get; set; }

        public double RainfallIndexmm => RainfallIndex == 0
            ? 0
            : Math.Pow(10.0, (RainfallIndex - 108.8805792) / 73.67120307);
    }

    public class BuienRadar
    {
        private TimeSpan _cacheTime;
        private double _lat;
        private double _lon;
        private Stopwatch _timeSinceFetch;
        private int _updateRunning;

        /// <summary>
        ///     Constructor.
        /// </summary>
        public BuienRadar()
        {
            Setup(TimeSpan.FromMinutes(1));
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cacheTime">Caching duration</param>
        public BuienRadar(TimeSpan cacheTime)
        {
            Setup(cacheTime);
        }

        private List<TimeRain> RainfallPrediction { get; set; }

        private void Setup(TimeSpan cacheTime)
        {
            _cacheTime = cacheTime;
            _timeSinceFetch = new Stopwatch();
            RainfallPrediction = new List<TimeRain>();
        }

        private string FetchRaw(double lat, double lon, TimeSpan timeout)
        {
            if (Math.Abs(_lat - lat) > 1e-8 || Math.Abs(_lon - lon) > 1e-8)
            {
                _lat = lat;
                _lon = lon;
                ClearCache();
            }

            var data = FetchRawInner(_lat, _lon, timeout);
            if (data != null) return data;
            // First fetch failed. Sometimes it seems to help to fetch a slightly different lat/lon
            Thread.Sleep(2000);
            data = FetchRawInner(_lat + 0.0000001, _lon + 0.0000001, timeout);

            return data;
        }


        /// <summary>
        /// Returns how far in the future the rainfall is predicted
        /// </summary>
        /// <returns>Timespan of prediction information</returns>
        public TimeSpan DataUntil()
        {
            PrunePrediction();
            return TimeSpan.FromMinutes(5 * RainfallPrediction.Count);
        }

        /// <summary>
        /// Indicates if rainfall predictions are known beyond 30 minutes in the future
        /// </summary>
        /// <returns>True if prediction information time > 30 min </returns>
        public bool Data30Mins()
        {
            return RainfallPrediction.Count >= 6;
        }

        /// <summary>
        /// Indicates if rainfall predictions are known beyond 90 minutes in the future
        /// </summary>
        /// <returns>True if prediction information time > 90 min </returns>
        public bool Data90Mins()
        {
            return RainfallPrediction.Count >= 18;
        }

        private string FetchRawInner(double lat, double lon, TimeSpan timeout)
        {
            try
            {
                NetworkUtils.WaitForConnection(timeout);
                var urlAddress = string.Format(CultureInfo.InvariantCulture,
                    "http://gadgets.buienradar.nl/data/raintext?lat={0}&lon={1}", lat, lon);

                Debug.WriteLine("Fetching buienradar data from url '" + urlAddress + "\'");
                var request = (HttpWebRequest) WebRequest.Create(urlAddress);
                request.Timeout = (int) timeout.TotalMilliseconds;
                var response = (HttpWebResponse) request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Debug.WriteLine("Error getting response from Buienradar");
                    return null; // Skip if no correct response received
                }
                var receiveStream = response.GetResponseStream();

                if (receiveStream == null) return null;
                StreamReader readStream = null;
                readStream = response.CharacterSet == null
                    ? new StreamReader(receiveStream)
                    : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                var data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
                return string.IsNullOrWhiteSpace(data) ? null : data;
            }
            catch
            {
                Debug.WriteLine("Error getting response from Buienradar");
                return null;
            }
        }

        private void ClearCache()
        {
            RainfallPrediction.Clear();
        }

        private void PrunePrediction()
        {
            foreach (var timeRain in RainfallPrediction.ToArray())
                if (timeRain.Time + TimeSpan.FromMinutes(5) <= DateTime.Now)
                    RainfallPrediction.Remove(timeRain);
        }

        /// <summary>
        ///     Asynchronous fetch of rainfall data
        /// </summary>
        /// <param name="latitude">Location latitude within Netherlands</param>
        /// <param name="longitude">Location longitude within Netherlands</param>
        /// <returns>List of rainfall (in mm) per 5 minute interval</returns>
        public async Task<List<TimeRain>> UpdateAsync(double latitude, double longitude)
        {
            return await UpdateAsync(latitude, longitude, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        ///     Asynchronous fetch of rainfall data
        /// </summary>
        /// <param name="latitude">Location latitude within Netherlands</param>
        /// <param name="longitude">Location longitude within Netherlands</param>
        /// <param name="timeout">Timeout for fetching web-data</param>
        /// <returns>List of rainfall (in mm) per 5 minute interval</returns>
        public async Task<List<TimeRain>> UpdateAsync(double latitude, double longitude, TimeSpan timeout)
        {
            // Only update if not currently updating
            if (Interlocked.CompareExchange(ref _updateRunning, 1, 0) != 0)
            {
                Debug.Write("Buienradar update already running, skipping ");
                return null;
            }

            try
            {
                Debug.Write("Starting BuienRadar update");
                return await Task.Run(() => { return Update(latitude, longitude, timeout); });
            }
            finally
            {
                Interlocked.Exchange(ref _updateRunning, 0);
                Debug.WriteLine("BuienRadar update done");
            }
        }

        /// <summary>
        ///     Synchronous fetch of rainfall data
        /// </summary>
        /// <param name="latitude">Location latitude within Netherlands</param>
        /// <param name="longitude">Location longitude within Netherlands</param>
        /// <returns>List of rainfall (in mm) per 5 minute interval</returns>
        public List<TimeRain> Update(double latitude, double longitude)
        {
            return Update(latitude, longitude, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        ///     Synchronous fetch of rainfall data
        /// </summary>
        /// <param name="latitude">Location latitude within Netherlands</param>
        /// <param name="longitude">Location longitude within Netherlands</param>
        /// <param name="timeout">Timeout for fetching web-data</param>
        /// <returns>List of rainfall (in mm) per 5 minute interval</returns>
        public List<TimeRain> Update(double latitude, double longitude, TimeSpan timeout)
        {
            PrunePrediction();

            if (_timeSinceFetch.IsRunning && _timeSinceFetch.Elapsed < _cacheTime && RainfallPrediction.Count > 0)
            {
                Debug.WriteLine("Used cached values");
                return RainfallPrediction;
            }

            if (!_timeSinceFetch.IsRunning) _timeSinceFetch.Start();
            Debug.WriteLine("Fetched new values");

            var data = FetchRaw(latitude, longitude, timeout);
            if (data == null) return RainfallPrediction; // If no data was received, skip            
            var lines = data.Split('\r', '\n');
            if (lines.Length == 0) return RainfallPrediction;


            RainfallPrediction.Clear();

            foreach (var line in lines)
            {
                // Try to parse each line
                if (string.IsNullOrWhiteSpace(line)) continue; // If line is empty, skip
                var split = line.Split('|');

                if (split.Length < 2) continue; // If zero or more separators are found, skip
                TimeSpan time;
                int rainfallIndex;
                if (string.IsNullOrWhiteSpace(split[0]))
                    rainfallIndex = 0; // If rain value is missing, assume no rain (Or should we skip this one?)
                if (!int.TryParse(split[0], out rainfallIndex)) continue; // if rain value is invalid value, skip
                if (!TimeSpan.TryParseExact(split[1], @"h\:m", CultureInfo.InvariantCulture, out time))
                    continue; // if time is invalid value, skip

                // Add to list
                RainfallPrediction.Add(new TimeRain {RainfallIndex = rainfallIndex, Time = DateTime.Today + time});
            }
            _timeSinceFetch.Restart();

            PrunePrediction();
            return RainfallPrediction;
        }
    }
}