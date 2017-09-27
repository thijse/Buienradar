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

namespace Buienradar.Utils
{
    public class GenericMinMax<T>
    {
        public static T GetMinValue()
        {
            object MinValue = default(T);
            var typeCode = Type.GetTypeCode(typeof (T));
            switch (typeCode)
            {
                case TypeCode.Byte:
                    MinValue = byte.MinValue;
                    break;
                case TypeCode.Char:
                    MinValue = char.MinValue;
                    break;
                case TypeCode.DateTime:
                    MinValue = DateTime.MinValue;
                    break;
                case TypeCode.Decimal:
                    MinValue = decimal.MinValue;
                    break;
                case TypeCode.Double:
                    MinValue = decimal.MinValue;
                    break;
                case TypeCode.Int16:
                    MinValue = short.MinValue;
                    break;
                case TypeCode.Int32:
                    MinValue = int.MinValue;
                    break;
                case TypeCode.Int64:
                    MinValue = long.MinValue;
                    break;
                case TypeCode.SByte:
                    MinValue = sbyte.MinValue;
                    break;
                case TypeCode.Single:
                    MinValue = float.MinValue;
                    break;
                case TypeCode.UInt16:
                    MinValue = ushort.MinValue;
                    break;
                case TypeCode.UInt32:
                    MinValue = uint.MinValue;
                    break;
                case TypeCode.UInt64:
                    MinValue = ulong.MinValue;
                    break;
                default:
                    MinValue = default(T); //set default value
                    break;
            }

            return (T) MinValue;
        }

        public static T GetMaxValue()
        {
            object maxValue = default(T);
            var typeCode = Type.GetTypeCode(typeof (T));
            switch (typeCode)
            {
                case TypeCode.Byte:
                    maxValue = byte.MaxValue;
                    break;
                case TypeCode.Char:
                    maxValue = char.MaxValue;
                    break;
                case TypeCode.DateTime:
                    maxValue = DateTime.MaxValue;
                    break;
                case TypeCode.Decimal:
                    maxValue = decimal.MaxValue;
                    break;
                case TypeCode.Double:
                    maxValue = decimal.MaxValue;
                    break;
                case TypeCode.Int16:
                    maxValue = short.MaxValue;
                    break;
                case TypeCode.Int32:
                    maxValue = int.MaxValue;
                    break;
                case TypeCode.Int64:
                    maxValue = long.MaxValue;
                    break;
                case TypeCode.SByte:
                    maxValue = sbyte.MaxValue;
                    break;
                case TypeCode.Single:
                    maxValue = float.MaxValue;
                    break;
                case TypeCode.UInt16:
                    maxValue = ushort.MaxValue;
                    break;
                case TypeCode.UInt32:
                    maxValue = uint.MaxValue;
                    break;
                case TypeCode.UInt64:
                    maxValue = ulong.MaxValue;
                    break;
                default:
                    maxValue = default(T); //set default value
                    break;
            }

            return (T) maxValue;
        }
    }
}