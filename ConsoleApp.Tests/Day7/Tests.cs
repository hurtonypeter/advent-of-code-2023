using ConsoleApp.Day7;

namespace ConsoleApp.Tests.Day7;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day7/example.txt").Should().Be(6440);
    }
    
    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day7/example.txt").Should().Be(5905);
    }
    
    [Theory]
    [InlineData("AAAAA", Parts.HandType.FiveOfAKind)]
    [InlineData("AAAJJ", Parts.HandType.FiveOfAKind)]
    [InlineData("AAJJJ", Parts.HandType.FiveOfAKind)]
    [InlineData("AJJJJ", Parts.HandType.FiveOfAKind)]
    [InlineData("AAABB", Parts.HandType.FullHouse)]
    [InlineData("AABBJ", Parts.HandType.FullHouse)]
    [InlineData("AAAAB", Parts.HandType.FourOfAKind)]
    [InlineData("AAABJ", Parts.HandType.FourOfAKind)]
    [InlineData("AABJJ", Parts.HandType.FourOfAKind)]
    [InlineData("ABJJJ", Parts.HandType.FourOfAKind)]
    [InlineData("AAABC", Parts.HandType.ThreeOfAKind)]
    [InlineData("AAJBC", Parts.HandType.ThreeOfAKind)]
    [InlineData("AJJBC", Parts.HandType.ThreeOfAKind)]
    [InlineData("AABBD", Parts.HandType.TwoPairs)]
    [InlineData("AABCD", Parts.HandType.OnePair)]
    [InlineData("ABCDJ", Parts.HandType.OnePair)]
    [InlineData("ABCDE", Parts.HandType.HighCard)]
    public void JokerHandType_Ok(string draw, Parts.HandType expectedType)
    {
        var parser = new Parts.PartTwoHandParser();

        parser.GetHandType(draw).Should().Be(expectedType);
    }
    
    [Theory]
    [InlineData("23456", 01_02_03_04_05_06)]
    [InlineData("22456", 02_02_02_04_05_06)]
    [InlineData("2245A", 02_02_02_04_05_14)]
    [InlineData("234KA", 01_02_03_04_13_14)]
    public void HandValueWithPartOneParser_Ok(string draw, ulong expectedValue)
    {
        var parser = new Parts.PartOneHandParser();

        parser.GetHandValue(draw, parser.GetHandType(draw)).Should().Be(expectedValue);
    }
    
    [Theory]
    [InlineData("2345J", 02_02_03_04_05_01)]
    [InlineData("2245J", 04_02_02_04_05_01)]
    [InlineData("2245A", 02_02_02_04_05_14)]
    [InlineData("234KA", 01_02_03_04_13_14)]
    public void HandValueWithPartTwoParser_Ok(string draw, ulong expectedValue)
    {
        var parser = new Parts.PartTwoHandParser();

        parser.GetHandValue(draw, parser.GetHandType(draw)).Should().Be(expectedValue);
    }
}