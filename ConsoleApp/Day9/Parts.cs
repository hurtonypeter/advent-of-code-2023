namespace ConsoleApp.Day9;

public static class Parts
{
    private static List<List<int>> ParseInput(string fileName)
    {
        var lines = File.ReadAllLines(fileName);

        return lines.Select(x =>
                x.Split(' ')
                    .Select(int.Parse)
                    .ToList())
            .ToList();
    }

    private static int ExtrapolateSeries(List<int> series, Func<List<List<int>>, int> extrapolationMethod)
    {
        var currentList = new List<int>(series);
        var derivedLists = new List<List<int>>(new[] {series});

        while (currentList.Any(x => x != 0) && currentList.Count > 1)
        {
            var newList = new List<int>();
            for (var i = 0; i < currentList.Count - 1; i++)
            {
                newList.Add(currentList[i + 1] - currentList[i]);
            }

            derivedLists.Add(newList);
            currentList = newList;
        }

        return extrapolationMethod(derivedLists);
    }

    private static int ExtrapolateSeriesForward(List<List<int>> derivedLists)
    {
        for (var i = derivedLists.Count - 2; i >= 0; i--)
        {
            derivedLists[i].Add(derivedLists[i].Last() + derivedLists[i + 1].Last());
        }

        return derivedLists[0].Last();
    }
    
    private static int ExtrapolateSeriesBackwards(List<List<int>> derivedLists)
    {
        for (var i = derivedLists.Count - 2; i >= 0; i--)
        {
            derivedLists[i].Insert(0, derivedLists[i].First() - derivedLists[i + 1].First());
        }

        return derivedLists[0].First();
    }

    public static int One(string fileName = "Day9/input.txt")
    {
        var series = ParseInput(fileName);

        var extrapolations = series
            .Select(x => ExtrapolateSeries(x, ExtrapolateSeriesForward))
            .ToList();

        var sum = extrapolations.Sum();

        Console.WriteLine("Day 9 Task 1 answer is: " + sum);

        return sum;
    }

    public static int Two(string fileName = "Day9/input.txt")
    {
        var series = ParseInput(fileName);

        var extrapolations = series
            .Select(x => ExtrapolateSeries(x, ExtrapolateSeriesBackwards))
            .ToList();

        var sum = extrapolations.Sum();
        
        Console.WriteLine("Day 9 Task 2 answer is: " + sum);

        return sum;
    }
}