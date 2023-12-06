using ConsoleApp.Day2;

namespace ConsoleApp.Tests.Day2;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day2/example.txt").Should().Be(8);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day2/example.txt").Should().Be(2286);
    }
}