using System;
using System.Globalization;
using MobileDevelopment.Extensions;
using MobileDevelopment.Models;
using Xunit;
using Xunit.Abstractions;

namespace MobileDevelopment.UnitTesting.Coordinate
{
    public class CoordinateTest
    {
        private ITestOutputHelper _outputHelper;
        
        public CoordinateTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        
        [Fact]
        public void ToDecimalDegree_ShouldReturnCorrectDecimalDegreeFormat()
        {
            // Arrange
            var coordinate = new CoordinateMh("13°49'50\"S");
            const float expected = 13.830556f;
            
            // Act
            var actual = coordinate.ToDecimalDegree();
            
            _outputHelper.WriteLine("13°49'50\"S");
            _outputHelper.WriteLine("Converted to DD.DDDDD" + actual.ToString(CultureInfo.InvariantCulture));
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDegreeMinuteSecondFormat_ShouldReturnCorrectDecimalDegreeFormat()
        {
            // Arrange
            var coordinate = new CoordinateMh(17.355f, Direction.S);
            const string expected = "17°21'17\" S";
            
            // Act
            var actual = coordinate.GetDegreeMinuteSecondFormat();
            
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(Direction.S, 190, 10, 10)]
        [InlineData(Direction.S, 100, 60, 10)]
        [InlineData(Direction.S, -20, 59, 72)]
        [InlineData(Direction.W, -91, 59, 59)]
        [InlineData(Direction.W,  91, 59, 59)]
        [InlineData(Direction.S, -200, 59, 30)]
        public void Ctor_ShouldThrowArgumentException_WhenDegreeOrMinuteOrSecond_GreaterOrLessDiapason(Direction direction, int degree, uint minute, uint second)
        {
            // Act, Assert
            Assert.Throws<ArgumentException>(() => new CoordinateMh(direction, degree, minute, second));
        }

        [Fact]
        public void MinusOperator_ShouldReturnCorrectDistanceBetweenTwoOfCoordinates()
        {
            // Arrange
            var coordinateA = new CoordinateMh(10.122f, Direction.S);
            var coordinateB = new CoordinateMh(17.333f, Direction.S);
            var expected = new CoordinateMh(7.211f, Direction.S);
            
            // Act
            _outputHelper.WriteLine("Coordinate A DD MM SS: " + coordinateA.GetDecimalDegreeFormat());
            _outputHelper.WriteLine("Coordinate A DD.DDDDD: " + coordinateA.GetDegreeMinuteSecondFormat());
            _outputHelper.WriteLine("Coordinate B DD MM SS: " + coordinateB.GetDecimalDegreeFormat());
            _outputHelper.WriteLine("Coordinate B DD.DDDDD: " + coordinateB.GetDegreeMinuteSecondFormat());
            var actual = coordinateA - coordinateB;
            
            // Assert
            Assert.Equal(expected.Degree, actual.Degree);
            Assert.Equal(expected.Minute, actual.Minute);
            Assert.Equal(expected.Second, actual.Second);
            
            // output
            _outputHelper.WriteLine(" ");
            _outputHelper.WriteLine("Get distance between A and B coordinate");
            _outputHelper.WriteLine("Result DD MM SS: " + actual.GetDecimalDegreeFormat());
            _outputHelper.WriteLine("Result DD.DDDDD: " + actual.GetDegreeMinuteSecondFormat());
        }
    }
}