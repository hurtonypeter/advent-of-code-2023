using ConsoleApp.Day13;

namespace ConsoleApp.Tests.Day13;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day13/example1.txt").Should().Be(405);
        Parts.One("Day13/example2.txt").Should().Be(800);
        Parts.One().Should().Be(33520);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day13/example.txt").Should().Be(1);
    }
}