using ConsoleApp.Day8;

namespace ConsoleApp.Tests.Day8;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day8/example1.txt").Should().Be(2);
        Parts.One("Day8/example2.txt").Should().Be(6);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day8/example3.txt").Should().Be(6);
    }
}