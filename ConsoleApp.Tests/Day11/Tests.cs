using ConsoleApp.Day11;

namespace ConsoleApp.Tests.Day11;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day11/example.txt").Should().Be(374);
        Parts.One().Should().Be(10289334);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day11/example.txt", 10).Should().Be(1030);
        Parts.Two("Day11/example.txt", 100).Should().Be(8410);
        Parts.Two().Should().Be(649862989626);
    }
}