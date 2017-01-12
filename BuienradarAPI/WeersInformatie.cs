using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Buienradar.Utils;

namespace Buienradar
{
    public class WeersInformatie
    {
        private TimeSpan _cacheTime;
        private Stopwatch _timeSinceFetch;

        private int _updateRunning;
        private WeersInformatieBuienRadar _weersInformatieBuienRadar;

        /// <summary>
        ///     Constructor.
        /// </summary>
        public WeersInformatie()
        {
            Setup(TimeSpan.FromHours(1));
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cacheTime">Caching duration</param>
        public WeersInformatie(TimeSpan cacheTime)
        {
            Setup(cacheTime);
        }

        private void Setup(TimeSpan timeout)
        {
            _updateRunning = 0;
            _timeSinceFetch = new Stopwatch();
            _cacheTime = timeout;
        }


        /// <summary>
        ///     Update and return weersinformatie data synchronously
        /// </summary>
        /// <returns>Data from the selected weerstation</returns>
        private WeersInformatieBuienRadar UpdateData()
        {
            return UpdateData(TimeSpan.FromSeconds(120));
        }


        /// <summary>
        ///     Update and return weersinformatie data synchronously
        /// </summary>
        /// <param name="timeout">Timeout for getting data</param>
        /// <returns>Data from the selected weerstation</returns>
        private WeersInformatieBuienRadar UpdateData(TimeSpan timeout)
        {
            try
            {
                if (_weersInformatieBuienRadar == null || _timeSinceFetch.Elapsed > _cacheTime)
                {
                    NetworkUtils.WaitForConnection(timeout);
                    var urlString = "https://xml.buienradar.nl/";
                    var reader = new XmlTextReader(urlString);
                    //var reader =  Utils.NetworkUtils.XmlTextReader(urlString,timeout);
                    var buienradarnl = new WeersInformatieBuienRadar();
                    var serializer = new XmlSerializer(typeof(WeersInformatieBuienRadar));
                    _weersInformatieBuienRadar = (WeersInformatieBuienRadar) serializer.Deserialize(reader);
                    _timeSinceFetch.Restart();
                    return _weersInformatieBuienRadar;
                }
                return _weersInformatieBuienRadar;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }
        }

        /// <summary>
        ///     Update and return weersinformatie data asynchronously
        /// </summary>
        /// <returns>Data from the selected weerstation</returns>
        public async Task<WeersInformatieBuienRadar> UpdateAsync()
        {
            return await UpdateAsync(TimeSpan.FromSeconds(120));
        }


        /// <summary>
        ///     Update and return weersinformatie data asynchronously
        /// </summary>
        /// <param name="timeout">Timeout for getting data</param>
        /// <returns>Data from the selected weerstation</returns>
        public async Task<WeersInformatieBuienRadar> UpdateAsync(TimeSpan timeout)
        {
            // Only update if not currently updating
            if (Interlocked.CompareExchange(ref _updateRunning, 1, 0) != 0)
            {
                Debug.Write("WeerStation update already running, skipping ");
                return null;
            }
            try
            {
                Debug.Write("Starting WeerStation update");
                _weersInformatieBuienRadar = await Task.Run(() => UpdateData());
                Debug.WriteLine("WeerStation update done");
            }
            finally
            {
                Interlocked.Exchange(ref _updateRunning, 0);
            }
            return _weersInformatieBuienRadar;
        }

        /// <summary>
        ///     Find weather station closest to given coordinates
        /// </summary>
        /// <param name="latitude">Location latitude within Netherlands</param>
        /// <param name="longitude">Location longitude within Netherlands</param>
        /// <param name="weersInformatieBuienRadar">information fetched from buienradar</param>
        /// <returns>Closest weather station</returns>
        public Weerstation SelectClosestWeatherStation(double latitude, double longitude,
            WeersInformatieBuienRadar weersInformatieBuienRadar)
        {
            return SelectClosestWeatherStation(latitude, longitude,
                weersInformatieBuienRadar.Weergegevens.ActueelWeer.Weerstations);
        }

        /// <summary>
        ///     Find weather station closest to given coordinates
        /// </summary>
        /// <param name="latitude">Location latitude within Netherlands</param>
        /// <param name="longitude">Location longitude within Netherlands</param>
        /// <param name="weerstations">List of weather stations from which to select</param>
        /// <returns>Closest weather station</returns>
        public Weerstation SelectClosestWeatherStation(double latitude, double longitude, Weerstations weerstations)
        {
            var minDistance = double.MaxValue;
            var closestWeatherStation = new Weerstation();
            foreach (var weerstation in weerstations.Weerstation)
            {
                var distance = GeoDistance.DistanceKm(latitude, longitude, double.Parse(weerstation.Latitude),
                    double.Parse(weerstation.Longitude));
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closestWeatherStation = weerstation;
            }
            return closestWeatherStation;
        }
    }
}