using ConsoleApp.Day15;

namespace ConsoleApp.Tests.Day15;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day15/example1.txt").Should().Be(52);
        Parts.One("Day15/example2.txt").Should().Be(1320);
        Parts.One().Should().Be(522547);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day15/example2.txt").Should().Be(145);
        Parts.Two().Should().Be(229271);
    }
}