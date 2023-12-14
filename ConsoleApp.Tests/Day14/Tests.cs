using ConsoleApp.Day14;

namespace ConsoleApp.Tests.Day14;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day14/example.txt").Should().Be(136);
        Parts.One().Should().Be(109661);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day14/example.txt").Should().Be(1);
    }
}