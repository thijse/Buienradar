using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace BuienRadar
{
    public class TimeRain
    {
        public DateTime Time          { get; set; }
        public int RainfallIndex      { get; set; }
        //public double RainfallIndexmm { get { return RainfallIndex==0?0:Math.Pow(10.0,(RainfallIndex - 109.0)/32.0);  } }
        //public double RainfallIndexmm { get { return RainfallIndex == 0 ? 0 : Math.Pow(10.0, (RainfallIndex - 75.0) / 110.0); } }
        //public double RainfallIndexmm { get { return RainfallIndex == 0 ? 0 : Math.Pow(10.0, (RainfallIndex - 35.0) / 73.1); } }
        public double RainfallIndexmm { get { return RainfallIndex == 0 ? 0 : Math.Pow(10.0, (RainfallIndex - 108.8805792) / 73.67120307); } }
        
    }


    public class BuienRadar
    {
        private  double _lat;
        private  double _lon;
        private readonly Stopwatch _timeSinceFetch;
        private readonly TimeSpan _cacheTime;

        public List<TimeRain> RainfallPrediction { get; private set; }
        public List<RainGroup> RailfalList { get; private set; }


        public BuienRadar()
        {
            _cacheTime = TimeSpan.FromMinutes(1);
            _timeSinceFetch = new Stopwatch();
            RainfallPrediction = new List<TimeRain>();
            RailfalList = new List<RainGroup>();
        }

        public BuienRadar(TimeSpan cacheTime)
        {
            _cacheTime = cacheTime;
            _timeSinceFetch = new Stopwatch();
            RainfallPrediction = new List<TimeRain>();
            RailfalList = new List<RainGroup>();
        }


        public string FetchRaw(double lat, double lon)
        {
            if ( Math.Abs(_lat - lat) > 1e-8 || Math.Abs(_lon - lon) > 1e-8)
            {
                _lat = lat;
                _lon = lon;
                ClearCache();
            }

            var data = FetchRawInner(_lat, _lon);
            if (data != null) return data;
            Thread.Sleep(500);
            data = FetchRawInner(_lat+0.0000001, _lon+0.0000001);

            return data;
        }

        private string FetchRawInner(double lat, double lon)
        {
            try
            {
                //var urlAddress = string.Format(CultureInfo.InvariantCulture,"http://gps.buienradar.nl/getrr.php?lat={0}&lon={1}", lat, lon);
                var urlAddress = string.Format(CultureInfo.InvariantCulture, "http://gadgets.buienradar.nl/data/raintext?lat={0}&lon={1}", lat, lon);

                Debug.WriteLine("Fetching buienradar data from url '" + urlAddress + "\'");
                var request = (HttpWebRequest) WebRequest.Create(urlAddress);
                request.Timeout = 2000;
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
                Debug.WriteLine("Retrieved data " + data);
                return string.IsNullOrWhiteSpace(data)?null:data;
            }
            catch
            {
                Debug.WriteLine("Error getting response from Buienradar");
                return null;
            }
        }


        public void ClearCache()
        {            
            RainfallPrediction.Clear();
        }

        public List<TimeRain> Dummy()
        {
            RainfallPrediction.Clear();
            for (int i = 0; i < 24; i++)
            {
                var index = 0;
                if (i == 2         ) { index = 57; }
                if (i > 3 && i < 10) { index = 120; }
                var t =new TimeRain {RainfallIndex = index, Time = DateTime.Now + TimeSpan.FromMinutes(5*i)};
                RainfallPrediction.Add(t);
            }
            return RainfallPrediction;
        }
        public List<TimeRain> Parse(double lattitude, double longitude)
        {
            // todo prune cached prediction on outdated values 
            PrunePrediction();

            if (_timeSinceFetch.IsRunning && _timeSinceFetch.Elapsed < _cacheTime && RainfallPrediction.Count > 0)
            {
                Debug.WriteLine("Used cached values");


                return RainfallPrediction;
            }

            if (!_timeSinceFetch.IsRunning) _timeSinceFetch.Start();
            Debug.WriteLine("Fetched new values");

            var data = FetchRaw(lattitude, longitude);
            if (data == null) return RainfallPrediction; // If no data was received, skip            
            var lines = data.Split(new[] { '\r', '\n' });
            if (lines.Length == 0) return RainfallPrediction;



            RainfallPrediction.Clear();

            foreach (var line in lines)
            {
                // Try to parse each line
                if (string.IsNullOrWhiteSpace(line)) continue; // If line is empty, skip
                var split = line.Split(new[] { '|' });

                if (split.Length < 2) continue; // If zero or more separators are found, skip
                TimeSpan time;
                int rainfallIndex;
                if (string.IsNullOrWhiteSpace(split[0]))
                    rainfallIndex = 0; // If rain value is missing, assume no rain (Or should we skip this one?)
                if (!int.TryParse(split[0], out rainfallIndex)) continue; // if rain value is invalid value, skip
                if (!TimeSpan.TryParseExact(split[1], @"h\:m", CultureInfo.InvariantCulture, out time))
                    continue; // if time is invalid value, skip

                // Add to list
                RainfallPrediction.Add(new TimeRain { RainfallIndex = rainfallIndex, Time = DateTime.Today + time });
            }
            _timeSinceFetch.Restart();

            // todo prune prediction
            PrunePrediction();
            return RainfallPrediction;
        }

        private void PrunePrediction()
        {
            foreach (var timeRain in RainfallPrediction.ToArray())
            {
                if (timeRain.Time + TimeSpan.FromMinutes(5) <= DateTime.Now)
                {
                    RainfallPrediction.Remove(timeRain);
                }
            }
        }

        public string TextDescription()
        {
            
            if (RainfallPrediction.Count == 0) return "";
            var description = "";            
            if (RailfalList.Count == 0)
            {
                description = "Het gaan de komende twee uren niet regenen";
            }
            else
            {
                var duration = RailfalList[0].Duration;
                var durationText =  duration<=TimeSpan.FromMinutes(10) ? "heel even" : duration<=TimeSpan.FromMinutes(30) ?"een poosje":"een flinke tijd";
                description = "Het gaat "+ TimeInFuture(RailfalList[0].StartTime-DateTime.Now)+" "+durationText+ " " +RainIntensityToText(RailfalList[0]) + ".";

                if ((RailfalList.Count == 1) && RailfalList[0].EndTime <DateTime.Now+TimeSpan.FromHours(1)) { description += " Daarna blijft het een tijd droog";}
                if (RailfalList.Count == 2)  { description += " Later gaat het nog een keer " + RainIntensityToText(RailfalList[1])+ ".";}

            }
            return description;
        }

        public string ToSchoolText()
        {
            var beginSchool = new TimeSpan(8,0,0);
            var endSchool = new TimeSpan(8,30,0);
            foreach (var timeRain in RainfallPrediction)
            {
                if (timeRain.Time.TimeOfDay >= beginSchool && timeRain.Time.TimeOfDay <= endSchool &&
                    timeRain.RainfallIndex > 0)
                {
                    return "Dus doe maar een regenjas aan als je naar school gaat";
                }

            }
            return "";
        }



        int intensityGroup(double mmperhour)
        {
            if (mmperhour < 0.5)
                return 0; // motregen
            else if (mmperhour < 2)
                return 1; // zachte regen
            else if (mmperhour < 2)
                return 2; // flinke regen
            else if (mmperhour < 30)
                return 3; // zware regen
            else if (mmperhour < 80)
                return 4; // zwaar onweer
            return 5; // zeer hevig onweer
        }

 
        string RainIntensityToText(RainGroup raingroup)
        {
            var rainIntensity = intensityGroup(raingroup.Max);
            switch (rainIntensity)
            {
                case 0:
                    return "motregenen";
                        case 1:
                    return "zacht regenen";
                            case 2:
                    return "hard regenen";
                            case 3:
                    return "heel hard regenen";
                            case 4:
                    return "super hard regenen";
                        case 5:
                    return "super super hard regenen";
            }
            return "regenen";
        }


        string TimeInFuture(TimeSpan timeSpan)
        {



            var minutes = timeSpan.Minutes;
            var minutesDiv = minutes/5.0;
            int minutesDivRound = (int)Math.Round(minutesDiv);
            var minutesRound = minutesDivRound*5;
            timeSpan = new TimeSpan(timeSpan.Hours,minutesRound,0);

            if (timeSpan <= TimeSpan.Zero)
            {
                return "nu";
            }

            return (((minutesRound != minutes && (timeSpan < TimeSpan.FromMinutes(15))) ? "over ongeveer " :"over ") + ToText(timeSpan));
            //return ((minutesRound > minutes) ? "iets minder dan " : (minutesRound < minutes) ? "iets meer dan " : "") + ToText(timeSpan);

        }

        string ToText(TimeSpan timeSpan)
        {
            var description = "";
            if (timeSpan.Seconds == 0)
            {
                if (timeSpan.Hours > 0)
                {
                    description = timeSpan.Hours + " uur ";
                    if (timeSpan.Minutes > 0)
                    {
                        switch (timeSpan.Minutes)
                        {
                            case 15:
                                description += timeSpan.Hours +" en een kwartier";
                                break;
                            case 30:
                                if (timeSpan.Hours==1) { description = " anderhalf uur"; } else { description = timeSpan.Hours +" en een half uur";}
                                break;
                            case 45:
                                description += " en drie kwartier";
                                break;
                            default:
                                description += " en " + timeSpan.Minutes + " minuten";
                                break;
                        }
                    }
                }
                else
                {
                    if (timeSpan.Minutes > 0)
                    {
                        description = timeSpan.Minutes + " minuten";
                    }
                    else
                    {
                        description = "";
                    }
                }
            }
            else
            {
                if (timeSpan.Hours > 0)
                {
                    description = timeSpan.Hours + " uur ";
                    if (timeSpan.Minutes > 0)
                    {
                        description += ", " + timeSpan.Minutes + " minuten en " + timeSpan.Seconds + " seconden";
                    }
                    else
                    {
                        description += " en " + timeSpan.Seconds + " seconden";
                    }
                }
                else
                {
                    if (timeSpan.Minutes > 0)
                    {
                        description = timeSpan.Minutes + " minuten en " + timeSpan.Seconds + " seconden";
                    }
                    else
                    {
                        description = timeSpan.Seconds + " seconden";
                    }       
                }
            }
            return description;
        }

        public void ParseGroups()
        {
            RailfalList.Clear();
            for (var i = 0; i < RainfallPrediction.Count; i++)
            {
                if (RainfallPrediction[i].RainfallIndex > 0)
                {
                    i = MakeGroup(i);
                }
                
            }
        }

        private int MakeGroup(int start)
        {
            var rainGroup = new RainGroup {StartTime = RainfallPrediction[start].Time};
            RailfalList.Add(rainGroup);
            // determine how many slots should be added
            const int norainSlots = 2;
            var slots = 0;
            var items = 0;
            for (int i = start; i < RainfallPrediction.Count; i++)
            {
                //if (RainfallPrediction[i].Time+TimeSpan.FromMinutes(5)< DateTime.Now) continue;
                if (RainfallPrediction[i].RainfallIndex == 0) { slots++; } else { slots=0; items = i; }
                if (slots > norainSlots) { break; }
            }
            
            for (var i = start; i < items; i++)
            {
                rainGroup.Add(RainfallPrediction[i].RainfallIndexmm);
            }

            return items;

        }
    }

    public class RainGroup : StatList
    {
     
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get { return TimeSpan.FromMinutes(5*Count);}  }
        public DateTime EndTime { get { return StartTime + Duration;  }}
    }
}


    

