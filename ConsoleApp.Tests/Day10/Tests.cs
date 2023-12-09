using ConsoleApp.Day10;

namespace ConsoleApp.Tests.Day10;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day10/example.txt").Should().Be(1);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day10/example.txt").Should().Be(1);
    }
}