using ConsoleApp.DayX;

namespace ConsoleApp.Tests.DayX;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("DayX/example.txt").Should().Be(1);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("DayX/example.txt").Should().Be(1);
    }
}