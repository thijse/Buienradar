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
using Buienradar.Utils;
using BuienRadar;

namespace Buienradar
{
    public class RainPeriod : StatList
    {
        public DateTime StartTime { get; set; }

        public TimeSpan Duration => TimeSpan.FromMinutes(5*Count);

        public DateTime EndTime => StartTime + Duration;
    }

    public class RainPeriods
    {
        private List<TimeRain> _rainfallPrediction;

        public RainPeriods()
        {
            Periods = new List<RainPeriod>();
        }

        public List<RainPeriod> Periods { get; }


        /// <summary>
        ///     Create periods of rainfall with basic statistics
        /// </summary>
        /// <param name="rainfallPrediction"></param>
        /// <returns></returns>
        public List<RainPeriod> CreatePeriods(List<TimeRain> rainfallPrediction)
        {
            _rainfallPrediction = rainfallPrediction;
            Periods.Clear();
            for (var i = 0; i < _rainfallPrediction.Count; i++)
                if (_rainfallPrediction[i].RainfallIndex > 0)
                    i = CreateGroup(i);
            return Periods;
        }

        private int CreateGroup(int start)
        {
            var rainGroup = new RainPeriod {StartTime = _rainfallPrediction[start].Time};
            Periods.Add(rainGroup);
            // determine how many slots should be added
            const int norainSlots = 2;
            var slots = 0;
            var items = 0;
            for (var i = start; i < _rainfallPrediction.Count; i++)
            {
                if (_rainfallPrediction[i].RainfallIndex == 0)
                {
                    slots++;
                }
                else
                {
                    slots = 0;
                    items = i;
                }
                if (slots > norainSlots)
                    break;
            }

            for (var i = start; i < items; i++)
                rainGroup.Add(_rainfallPrediction[i].RainfallIndexmm);
            return items;
        }
    }
}