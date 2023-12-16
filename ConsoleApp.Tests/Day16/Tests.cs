using ConsoleApp.Day16;

namespace ConsoleApp.Tests.Day16;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day16/example.txt").Should().Be(46);
        Parts.One().Should().Be(7927);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day16/example.txt").Should().Be(51);
    }
}