﻿using System;
using System.Collections.Generic;

namespace Buienradar.Utils
{
    /// <summary>
    ///     Class contain statistics about min and max value
    /// </summary>
    public class StatList : List<double>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public StatList()
        {
            Min = double.MaxValue;
            Max = double.MinValue;
            Clear();
        }

        /// <summary>
        ///     Minimum value
        /// </summary>
        public double Min { get; private set; }

        /// <summary>
        ///     Maximum value
        /// </summary>
        public double Max { get; private set; }


        /// <summary>
        ///     Add a value
        /// </summary>
        /// <param name="x">value</param>
        public new void Add(double x)
        {
            base.Add(x);

            Max = Math.Max(Max, x);
            Min = Math.Min(Min, x);
        }

        /// <summary>
        ///     Calculate average
        /// </summary>
        /// <returns>average value</returns>
        public double Average()
        {
            if (Count == 0) return 0;
            double sum = 0;
            foreach (var value in this)
                sum += value;
            return (float) sum / Count;
        }

        /// <summary>
        ///     Calculate standard deviation
        /// </summary>
        /// <returns>standard deviation</returns>
        public double StdDev()
        {
            if (Count == 0) return 0;
            var avg = Average();
            double summedError = 0;
            foreach (var value in this)
                summedError += Math.Pow(value - avg, 2);
            return (float) Math.Sqrt(summedError / Count);
        }

        public double Median()
        {
            return this[Count / 2];
        }

        public double SortedMedian()
        {
            List<double> sortedArray = this;
            sortedArray.Sort();
            return sortedArray[sortedArray.Count / 2];
        }


        /// <summary>
        ///     Convert to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Count: {0} Min: {1} Max: {2} Avg: {3} StDev: {4}",
                Count, Min, Max, Average(), StdDev());
        }
    }
}