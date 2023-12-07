namespace ConsoleApp.Day7;

public static partial class Parts
{
    public abstract class HandParserBase
    {
        protected abstract Dictionary<char, int> CardValues { get; }

        public abstract HandType GetHandType(string draw);

        public ulong GetHandValue(string draw, HandType type)
        {
            var chars = draw.ToCharArray();
            var value = (ulong) type * (ulong) Math.Pow(10, 10);

            for (var i = 0; i < chars.Length; i++)
            {
                value += (ulong) CardValues[chars[chars.Length - i - 1]] * (ulong) Math.Pow(10, i * 2);
            }

            return value;
        }
    }

    public class PartOneHandParser : HandParserBase
    {
        protected override Dictionary<char, int> CardValues { get; } = new()
        {
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'T', 10},
            {'J', 11},
            {'Q', 12},
            {'K', 13},
            {'A', 14}
        };

        public override HandType GetHandType(string draw)
        {
            var chars = draw.ToCharArray();
            var groups = chars.GroupBy(x => x).ToList();

            return groups.Count switch
            {
                1 => HandType.FiveOfAKind,
                2 when groups.Any(x => x.Count() == 4) => HandType.FourOfAKind,
                2 => HandType.FullHouse,
                3 when groups.Any(x => x.Count() == 3) => HandType.ThreeOfAKind,
                3 => HandType.TwoPairs,
                4 => HandType.OnePair,
                _ => HandType.HighCard
            };
        }
    }

    public class PartTwoHandParser : HandParserBase
    {
        protected override Dictionary<char, int> CardValues { get; } = new()
        {
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'T', 10},
            {'J', 1},
            {'Q', 12},
            {'K', 13},
            {'A', 14}
        };

        public override HandType GetHandType(string draw)
        {
            var chars = draw.ToCharArray();
            var groups = chars.GroupBy(x => x).ToList();

            return groups.Count switch
            {
                1 => HandType.FiveOfAKind,
                2 when groups.Any(x => x.Key == 'J') => HandType.FiveOfAKind,
                2 when groups.Any(x => x.Count() == 4) => HandType.FourOfAKind,
                2 => HandType.FullHouse,
                3 when groups.Any(x => x.Count() == 3) && groups.Any(x => x.Key == 'J') => HandType.FourOfAKind,
                3 when groups.Any(x => x.Count() == 3) => HandType.ThreeOfAKind,
                3 when groups.Any(x => x.Key == 'J' && x.Count() == 2) => HandType.FourOfAKind,
                3 when groups.Any(x => x.Key == 'J') => HandType.FullHouse,
                3 => HandType.TwoPairs,
                4 when groups.Any(x => x.Key == 'J') => HandType.ThreeOfAKind,
                4 => HandType.OnePair,
                5 when groups.Any(x => x.Key == 'J') => HandType.OnePair,
                5 => HandType.HighCard,
                _ => throw new ArgumentOutOfRangeException(draw)
            };
        }
    }
}