using System;
using System.Text.RegularExpressions;
using MobileDevelopment.Extensions;

namespace MobileDevelopment.Models
{
    public class CoordinateMh
    {
        private uint _second;
        private int _degree;
        private uint _minute;
        public readonly Direction Direction;
        
        public CoordinateMh()
        {
            Direction = Direction.N;
            Degree = 0;
            Minute = 0;
            Second = 0;
        }

        public CoordinateMh(Direction direction, int degree, uint minute, uint second)
        {
            Direction = direction;
            Degree = degree;
            Minute = minute;
            Second = second;
        }

        public CoordinateMh(float decimalDegree, Direction direction)
        {
            Direction = direction;
            Degree = (int) Math.Truncate(decimalDegree);
            Minute = (uint) Math.Truncate((decimalDegree - Degree) * 60);
            Second = (uint) Math.Truncate(((decimalDegree - Degree) * 60 - Minute) * 60);
        }

        public CoordinateMh(string coordinate)
        {
            // handle direction.
            if (coordinate.Contains("S"))
            {
                Direction = Direction.S;
            }
            else if (coordinate.Contains("W"))
            {
                Direction = Direction.W;
            }
            else if (coordinate.Contains("E"))
            {
                Direction = Direction.E;
            }
            else if (coordinate.Contains("N"))
            {
                Direction = Direction.N;
            }
            else
            {
                throw new ArgumentException("Incorrect format, direction not exist.");
            }
            
            // remove the characters
            coordinate = Regex.Replace(coordinate, @"[^0-9.°'""]", ""); 
            
            // split the string.
            var pointArray = coordinate.Split('°', '\'', '\"', '.');

            Degree = int.Parse(pointArray[0]);
            Minute = uint.Parse(pointArray[1]);
            Second = uint.Parse(pointArray[2]);
        }

        public static CoordinateMh operator -(CoordinateMh a, CoordinateMh b)
        {
            if (a.Direction != b.Direction)
            {
                return null;
            }
            var middleDistance = b.ToDecimalDegree() - a.ToDecimalDegree();
            return new CoordinateMh(middleDistance, a.Direction);
        }
        
        public int Degree
        {
            get => _degree;
            private set
            {
                switch (value)
                {
                    case > 90:
                    case < -90 when Direction == Direction.W:
                        throw new ArgumentException("Value must be ∈ [-90, 90] for longitude.");
                    case < -180 when Direction == Direction.S:
                        throw new ArgumentException("Value must be ∈ [-180, 180] for latitude.");
                    default:
                        _degree = value;
                        break;
                }
            }
        }
        
        public uint Minute
        {
            get => _minute;
            private set
            {
                if (value > 59)
                {
                    throw new ArgumentException("Value must be ∈ [0, 59].");
                }
                _minute = value;
            }
        }
        
        public uint Second
        {
            get => _second;
            private set
            {
                if (value > 59)
                {
                    throw new ArgumentException("Value must be ∈ [0, 59].");
                }
                _second = value;
            }
        }
    }
}