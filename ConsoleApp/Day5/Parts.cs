using System.Diagnostics;

namespace ConsoleApp.Day5;

public static class Parts
{
    public record struct Range(long Start, long Length);

    private static (long From, long To) ToInterval(Range range)
    {
        return (range.Start, range.Start + range.Length - 1);
    }

    public record struct GardenMapEntry(Range Destination, Range Source);

    private static (List<long> Seeds, List<List<GardenMapEntry>> Maps) ParseInput(string fileName)
    {
        var blocks = File.ReadAllText(fileName)
            .Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        var seeds = blocks[0]
            .Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        var maps = new List<List<GardenMapEntry>>();
        for (var i = 1; i < blocks.Length; i++)
        {
            var lines = blocks[i].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var map = new List<GardenMapEntry>();
            for (var j = 1; j < lines.Length; j++)
            {
                var numbers = lines[j].Split(' ').Select(long.Parse).ToList();

                map.Add(new GardenMapEntry(
                    new Range(numbers[0], numbers[2]),
                    new Range(numbers[1], numbers[2])));
            }

            maps.Add(map);
        }

        return (seeds, maps);
    }

    private static bool IsInRange(long value, Range range)
    {
        return value >= range.Start && value < range.Start + range.Length;
    }

    public static Range? GetIntersection(Range range1, Range range2)
    {
        var start = Math.Max(range1.Start, range2.Start);
        var end = Math.Min(range1.Start + range1.Length - 1, range2.Start + range2.Length - 1);

        if (start <= end)
        {
            return new Range(start, end - start + 1);
        }

        return null;
    }

    public static long One(string fileName = "Day5/input.txt")
    {
        var (seeds, maps) = ParseInput(fileName);

        var minTarget = long.MaxValue;
        foreach (var seed in seeds)
        {
            var targetId = seed;

            foreach (var map in maps)
            {
                var mapItem = map.SingleOrDefault(x => IsInRange(targetId, x.Source));

                if (mapItem != default)
                {
                    targetId = mapItem.Destination.Start + (targetId - mapItem.Source.Start);
                }
            }

            if (targetId < minTarget)
            {
                minTarget = targetId;
            }
        }

        Console.WriteLine("Day 5 Task 1 answer is: " + minTarget);

        return minTarget;
    }

    public static long TwoBruteForce(string fileName = "Day5/input.txt")
    {
        var timer = new Stopwatch();
        timer.Start();

        var (seeds, maps) = ParseInput(fileName);

        var seedRanges = seeds
            .Chunk(2)
            .Select(x => new Range(x[0], x[1]))
            .ToList();

        var minTarget = long.MaxValue;
        foreach (var range in seedRanges)
        {
            for (var i = range.Start; i < range.Start + range.Length; i++)
            {
                var targetId = i;

                for (var j = 0; j < maps.Count; j++)
                {
                    GardenMapEntry? mapItem = null;
                    for (var k = 0; k < maps[j].Count; k++)
                    {
                        if (!IsInRange(targetId, maps[j][k].Source)) continue;
                        mapItem = maps[j][k];
                        break;
                    }

                    if (mapItem != null)
                    {
                        targetId = mapItem.Value.Destination.Start + (targetId - mapItem.Value.Source.Start);
                    }
                }

                if (targetId < minTarget)
                {
                    minTarget = targetId;
                }
            }

            Console.WriteLine($"Range done from: {range.Start} to: {range.Start + range.Length}");
        }

        timer.Stop();
        Console.WriteLine(
            $@"Day 5 Task 2 answer is: {minTarget}, took {timer.Elapsed:m\:ss\.fff}");

        return minTarget;
    }

    public static long Two(string fileName = "Day5/input.txt")
    {
        var (seeds, maps) = ParseInput(fileName);

        var seedRanges = seeds
            .Chunk(2)
            .Select(x => new Range(x[0], x[1]))
            .ToList();
        
        var targets = new List<Range>(seedRanges);
        foreach (var map in maps)
        {
            var newTargets = new List<Range>();
            
            foreach (var target in targets)
            {
                Range? intersection = null;
                GardenMapEntry? mapItem = null;
                foreach (var entry in map)
                {
                    intersection = GetIntersection(target, entry.Source);

                    if (intersection != null)
                    {
                        mapItem = entry;
                        break;
                    }
                }

                if (intersection != null)
                {
                    newTargets.AddRange(
                        ChunkAndMapIntervalByIntersection(
                            intersection.Value, target, mapItem!.Value));
                }
                else
                {
                    newTargets.Add(target);
                }
            }
            
            targets = new List<Range>(newTargets);
        }
        
        var minTarget = targets.MinBy(x => x.Start).Start;

        Console.WriteLine("Day 5 Task 2 answer is: " + minTarget);

        return minTarget;
    }

    public static IList<Range> ChunkAndMapIntervalByIntersection(
        Range intersection,
        Range target,
        GardenMapEntry entry)
    {
        var newTargets = new List<Range>
        {
            intersection with
            {
                Start = intersection.Start - (entry.Source.Start - entry.Destination.Start)
            }
        };

        if (target.Start < intersection.Start)
        {
            newTargets.Add(target with {Length = intersection.Start - target.Start});
        }

        if (target.Start + target.Length > intersection.Start + intersection.Length)
        {
            newTargets.Add(new Range(intersection.Start + intersection.Length,
                target.Start + target.Length - (intersection.Start + intersection.Length)));
        }

        return newTargets;
    }
}