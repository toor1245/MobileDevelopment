using System;
using System.Globalization;
using MobileDevelopment.Extensions;
using MobileDevelopment.Models;

namespace MobileDevelopment.Formatters
{
    public class CoordinateFormat : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            // Provide default formatting if arg is not an CoordinateXY.
            if (arg.GetType() != typeof(CoordinateMh) || !(fmt is "G" || fmt is "D"))
            {
                try
                {
                    return SoftwareFallback(fmt, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException($"The format of '{fmt}' is invalid.", e);
                }
            }

            // Convert argument to a coordinate.
            var result = (CoordinateMh) arg;

            return fmt switch
            {
                "G" => $"{result.Degree}°{result.Minute}'{result.Second}\" {result.Direction}",
                "D" => $"{result.ToDecimalDegree()} {result.Direction}",
                _ => SoftwareFallback(fmt, arg)
            };
            
            static string SoftwareFallback(string format, object arg)
            {
                if (arg is IFormattable formattable)
                {
                    return formattable.ToString(format, CultureInfo.CurrentCulture);
                }
                return arg != null ? arg.ToString() : string.Empty;
            }
        }
    }
}