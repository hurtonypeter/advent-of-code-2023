using ConsoleApp.Day12;

namespace ConsoleApp.Tests.Day12;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day12/example.txt").Should().Be(21);
        Parts.One().Should().Be(7541);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day12/example.txt").Should().Be(1);
    }
}