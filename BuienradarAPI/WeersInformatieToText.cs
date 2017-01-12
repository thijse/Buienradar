namespace Buienradar
{
    public static class WeersInformatieToText
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