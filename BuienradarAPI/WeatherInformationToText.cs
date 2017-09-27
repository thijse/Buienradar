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

namespace Buienradar
{
    public static class WeatherInformationToText
    {
        /// <summary>
        ///     Convert temperature from weather station to dutch textual representation + clothing advice :-)
        /// </summary>
        /// <param name="weerstation"></param>
        /// <returns>Dutch textual representation</returns>
        public static string TemperatureToText(Weerstation weerstation)
        {
            double temp;
            if (!double.TryParse(weerstation.Temperatuur10cm, out temp)) return "";
            if (temp < -2) return "Het is super koud, dus doe een winterjas en handschoenen aan";
            if (temp < 4) return "Het is koud, dus doe een winterjas aan";
            if (temp < 20) return "Het wordt redelijk weer, maar een jas is wel nodig";
            if (temp < 24) return "Het wordt lekker warm,  dus je hoeft geen jas aan";
            if (temp < 28) return "Het wordt erg warm,  dus je hoeft geen trui aan";
            return "Het wordt super warm,  dus een shirt is echt genoeg";
        }
    }
}