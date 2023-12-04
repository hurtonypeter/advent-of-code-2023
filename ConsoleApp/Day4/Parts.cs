namespace ConsoleApp.Day4;

public static class Parts
{
    private record struct Card(List<int> WinningNumbers, List<int> OwnedNumbers);

    private static IEnumerable<Card> ParseCards()
    {
        var lines = File.ReadAllLines("Day4/input.txt");

        foreach (var line in lines)
        {
            var numbers = line.Split(':')[1];
            var cardData = numbers.Split('|');

            var winningNumbers = cardData[0].Split(' ')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToList();
            var ownedNumbers = cardData[1].Split(' ')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToList();

            yield return new Card(winningNumbers, ownedNumbers);
        }
    }

    private static int ProcessCard(int index, IList<Card> cards)
    {
        if (index > cards.Count) return 0;
        
        var matches = cards[index].WinningNumbers.Intersect(cards[index].OwnedNumbers).Count();

        var sum = matches;

        for (var i = 1; i <= matches ; i++)
        {
            sum += ProcessCard(index + i, cards);
        }

        return sum;
    }

    public static void One()
    {
        var cards = ParseCards().ToList();

        var points = cards.Aggregate(0, (acc, card) =>
        {
            var matches = card.WinningNumbers.Intersect(card.OwnedNumbers).Count();
            return matches > 0 ? acc + (int) Math.Pow(2, matches - 1) : acc;
        });

        Console.WriteLine("Day 4 Task 1 answer is: " + points);
    }

    public static void Two()
    {
        var cards = ParseCards().ToList();

        var totalCards = cards.Count + cards.Select((_, i) => ProcessCard(i, cards)).Sum();

        Console.WriteLine("Day 4 Task 2 answer is: " + totalCards);
    }
}