namespace ConsoleApp.Day6;

public static class Parts
{
    private record struct Race(ulong Time, ulong DistanceRecord);

    private static IEnumerable<Race> ParseFileOne(string fileName)
    {
        var lines = File.ReadAllLines(fileName);

        var times = lines[0]
            .Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(ulong.Parse)
            .ToArray();

        var distances = lines[1]
            .Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(ulong.Parse)
            .ToArray();

        for (var i = 0; i < times.Length; i++)
        {
            yield return new Race(times[i], distances[i]);
        }
    }

    private static Race ParseFileTwo(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        
        var time = lines[0]
            .Split(':')[1]
            .Replace(" ", string.Empty);

        var distance = lines[1]
            .Split(':')[1]
            .Replace(" ", string.Empty);

        return new Race(ulong.Parse(time), ulong.Parse(distance));
    }

    private static int CalculateRaceWinOptions(Race race)
    {
        var raceWinOptions = 0;
        for (ulong i = 1; i < race.Time; i++)
        {
            var remainingTimeToDrive = race.Time - i;

            if (i * remainingTimeToDrive > race.DistanceRecord)
            {
                raceWinOptions++;
            }
        }

        return raceWinOptions;
    }

    public static int One(string fileName = "Day6/input.txt")
    {
        var races = ParseFileOne(fileName).ToList();

        var result = 1;

        foreach (var race in races)
        {
            var raceWinOptions = CalculateRaceWinOptions(race);

            result *= raceWinOptions;
        }

        Console.WriteLine("Day 6 Task 1 answer is: " + result);

        return result;
    }

    public static int Two(string fileName = "Day6/input.txt")
    {
        var race = ParseFileTwo(fileName);

        var raceWinOptions = CalculateRaceWinOptions(race);
        
        Console.WriteLine("Day 6 Task 1 answer is: " + raceWinOptions);

        return raceWinOptions;
    }

}