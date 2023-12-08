namespace ConsoleApp.Day7;

public static partial class Parts
{
    public enum HandType
    {
        HighCard = 1,
        OnePair = 2,
        TwoPairs = 3,
        ThreeOfAKind = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7
    }

    private record struct Hand(string Draw, int Bid, HandType Type, ulong Value);

    private static IEnumerable<Hand> ParseHands(string fileName, HandParserBase parser)
    {
        var lines = File.ReadAllLines(fileName);

        foreach (var line in lines)
        {
            var handData = line.Split(' ');
            var draw = handData[0];
            var bid = int.Parse(handData[1]);
            var type = parser.GetHandType(draw);
            var value = parser.GetHandValue(draw, type);

            yield return new Hand(draw, bid, type, value);
        }
    }

    private static int CalculateTotalWinnings(string fileName, HandParserBase parser)
    {
        var hands = ParseHands(fileName, parser).ToList();

        var orderedHands = hands.OrderBy(x => x.Value).ToList();

        var total = orderedHands.Select((t, i) => t.Bid * (i + 1)).Sum();

        return total;
    }

    public static int One(string fileName = "Day7/input.txt")
    {
        var total = CalculateTotalWinnings(fileName, new PartOneHandParser());

        Console.WriteLine("Day 7 Task 1 answer is: " + total);

        return total;
    }

    public static int Two(string fileName = "Day7/input.txt")
    {
        var total = CalculateTotalWinnings(fileName, new PartTwoHandParser());

        Console.WriteLine("Day 7 Task 2 answer is: " + total);

        return total;
    }
}