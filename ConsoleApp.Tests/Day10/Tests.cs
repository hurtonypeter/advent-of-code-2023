using ConsoleApp.Day10;

namespace ConsoleApp.Tests.Day10;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day10/example1.txt").Should().Be(8);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day10/example2.txt").Should().Be(4);
        Parts.Two("Day10/example3.txt").Should().Be(8);
        Parts.Two("Day10/example4.txt").Should().Be(10);
    }
}