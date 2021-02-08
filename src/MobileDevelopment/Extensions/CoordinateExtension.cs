using MobileDevelopment.Formatters;
using MobileDevelopment.Models;

namespace MobileDevelopment.Extensions
{
    public static class CoordinateExtension
    {
        private static readonly CoordinateFormat CoordinateFormat;
        
        static CoordinateExtension() => CoordinateFormat = new CoordinateFormat();

        public static float ToDecimalDegree(this CoordinateMh coordinateMh)
        {
            float m = coordinateMh.Minute;
            float s = coordinateMh.Second;
            return coordinateMh.Degree + m / 60 + s / 3600;
        }
        
        public static string GetDegreeMinuteSecondFormat(this CoordinateMh coordinateXy)
        {
            return string.Format(CoordinateFormat, "{0:G}", coordinateXy);
        }
        
        public static string GetDecimalDegreeFormat(this CoordinateMh coordinateXy)
        {
            return string.Format(CoordinateFormat, "{0:D}", coordinateXy);
        }
    }
}