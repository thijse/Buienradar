﻿using System;
using System.Linq;

namespace Buienradar.Utils
{
    public static class StringUtils
    {
        public static string[] Wrap(string text, int max)
        {
            var charCount = 0;
            var lines = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return lines.GroupBy(w => (charCount += (charCount%max + w.Length + 1 >= max
                ? max - charCount%max
                : 0) + w.Length + 1)/max)
                .Select(g => string.Join(" ", g.ToArray()))
                .ToArray();
        }
    }
}