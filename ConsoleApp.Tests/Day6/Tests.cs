using ConsoleApp.Day6;

namespace ConsoleApp.Tests.Day6;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day6/example.txt").Should().Be(288);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day6/example.txt").Should().Be(71503);
    }
}