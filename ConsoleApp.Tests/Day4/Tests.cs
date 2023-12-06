using ConsoleApp.Day4;

namespace ConsoleApp.Tests.Day4;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day4/example.txt").Should().Be(13);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day4/example.txt").Should().Be(30);
    }
}