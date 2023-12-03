using ConsoleApp.Day3;

namespace ConsoleApp.Tests;

public class Day3
{
    [Theory]
    [InlineData(2, 4, 2, 0, 0, false)]
    [InlineData(2, 4, 2, 1, 0, false)]
    [InlineData(2, 4, 2, 2, 0, false)]
    [InlineData(2, 4, 2, 3, 0, false)]
    [InlineData(2, 4, 2, 4, 0, false)]
    [InlineData(2, 4, 2, 5, 0, false)]
    [InlineData(2, 4, 2, 6, 0, false)]
    [InlineData(2, 4, 2, 0, 1, false)]
    [InlineData(2, 4, 2, 1, 1, true)]
    [InlineData(2, 4, 2, 2, 1, true)]
    [InlineData(2, 4, 2, 3, 1, true)]
    [InlineData(2, 4, 2, 4, 1, true)]
    [InlineData(2, 4, 2, 5, 1, true)]
    [InlineData(2, 4, 2, 6, 1, false)]
    [InlineData(2, 4, 2, 0, 2, false)]
    [InlineData(2, 4, 2, 1, 2, true)]
    [InlineData(2, 4, 2, 2, 2, true)]
    [InlineData(2, 4, 2, 3, 2, true)]
    [InlineData(2, 4, 2, 4, 2, true)]
    [InlineData(2, 4, 2, 5, 2, true)]
    [InlineData(2, 4, 2, 6, 2, false)]
    [InlineData(2, 4, 2, 0, 3, false)]
    [InlineData(2, 4, 2, 1, 3, true)]
    [InlineData(2, 4, 2, 2, 3, true)]
    [InlineData(2, 4, 2, 3, 3, true)]
    [InlineData(2, 4, 2, 4, 3, true)]
    [InlineData(2, 4, 2, 5, 3, true)]
    [InlineData(2, 4, 2, 6, 3, false)]
    [InlineData(2, 4, 2, 0, 4, false)]
    [InlineData(2, 4, 2, 1, 4, false)]
    [InlineData(2, 4, 2, 2, 4, false)]
    [InlineData(2, 4, 2, 3, 4, false)]
    [InlineData(2, 4, 2, 4, 4, false)]
    [InlineData(2, 4, 2, 5, 4, false)]
    [InlineData(2, 4, 2, 6, 4, false)]
    [InlineData(2, 4, 2, 10, 10, false)]
    public void IsCloseTo(int vectorX1, int vectorX2, int vectorY, int coordinateX, int coordinateY, bool expected)
    {
        var vector = new Parts.HorizontalVector(vectorX1, vectorX2, vectorY);
        var coordinate = new Parts.Coordinate(coordinateX, coordinateY);

        Parts.IsCoordinateNextToHorizontalVector(coordinate, vector).Should().Be(expected);
    }
}