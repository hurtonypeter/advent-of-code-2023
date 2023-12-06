using ConsoleApp.Day1;

namespace ConsoleApp.Tests.Day1;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day1/example1.txt").Should().Be(142);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day1/example2.txt").Should().Be(281);
    }
}