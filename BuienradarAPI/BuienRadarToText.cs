using System;
using System.Collections.Generic;

namespace Buienradar
{
    public static class BuienRadarToText
    {
        /// <summary>
        ///     Dutch textual description of rain periods in the coming two hours
        /// </summary>
        /// <param name="rainPeriods">List of rain periods</param>
        /// <returns></returns>
        public static string TextDescription(List<RainPeriod> rainPeriods)
        {
            var description = "";
            if (rainPeriods.Count == 0)
            {
                return "Het gaat de komende twee uur niet regenen.";
            }
            var duration = rainPeriods[0].Duration;
            var durationText = duration <= TimeSpan.FromMinutes(10)
                ? "heel even"
                : duration <= TimeSpan.FromMinutes(30) ? "een poosje" : "een flinke tijd";
            description = "Het gaat " + TimeInFuture(rainPeriods[0].StartTime - DateTime.Now) + " " + durationText +
                          " " + RainIntensityToText(rainPeriods[0]) + ".";

            if ((rainPeriods.Count == 1) && rainPeriods[0].EndTime < DateTime.Now + TimeSpan.FromHours(1))
            {
                description += " Daarna blijft het een tijd droog";
            }
            if (rainPeriods.Count == 2)
            {
                description += " Later gaat het nog een keer " + RainIntensityToText(rainPeriods[1]) + ".";
            }
            return description;
        }


        private static int IntensityGroup(double mmperhour)
        {
            if (mmperhour < 0.5)
                return 0; // motregen
            if (mmperhour < 2)
                return 1; // zachte regen
            if (mmperhour < 2)
                return 2; // flinke regen
            if (mmperhour < 30)
                return 3; // zware regen
            if (mmperhour < 80)
                return 4; // zwaar onweer
            return 5; // zeer hevig onweer
        }


        private static string RainIntensityToText(RainPeriod raingroup)
        {
            var rainIntensity = IntensityGroup(raingroup.Max);
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


        private static string TimeInFuture(TimeSpan timeSpan)
        {
            var minutes = timeSpan.Minutes;
            var minutesDiv = minutes/5.0;
            var minutesDivRound = (int) Math.Round(minutesDiv);
            var minutesRound = minutesDivRound*5;
            timeSpan = new TimeSpan(timeSpan.Hours, minutesRound, 0);

            if (timeSpan <= TimeSpan.Zero)
            {
                return "nu";
            }

            return (minutesRound != minutes && (timeSpan < TimeSpan.FromMinutes(15)) ? "over ongeveer " : "over ") +
                   ToText(timeSpan);
        }

        private static string ToText(TimeSpan timeSpan)
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
                                description += timeSpan.Hours + " en een kwartier";
                                break;
                            case 30:
                                if (timeSpan.Hours == 1)
                                {
                                    description = " anderhalf uur";
                                }
                                else
                                {
                                    description = timeSpan.Hours + " en een half uur";
                                }
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
    }
}