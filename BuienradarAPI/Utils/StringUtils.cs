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