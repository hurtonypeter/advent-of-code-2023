using ConsoleApp.Day9;

namespace ConsoleApp.Tests.Day9;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day9/example.txt").Should().Be(114);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day9/example.txt").Should().Be(2);
    }
}