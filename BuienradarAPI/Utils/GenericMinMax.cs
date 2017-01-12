﻿using System;

namespace Buienradar.Utils
{
    public class GenericMinMax<T>
    {
        public static T GetMinValue()
        {
            object MinValue = default(T);
            var typeCode = Type.GetTypeCode(typeof(T));
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
            var typeCode = Type.GetTypeCode(typeof(T));
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