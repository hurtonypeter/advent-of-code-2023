namespace ConsoleApp.Day12;

public static class Parts
{
    private record struct Row(string Springs, List<int> DamagedSpringsChecksums);

    private static List<Row> ParseInput(string fileName)
    {
        var lines = File.ReadAllLines(fileName);

        return lines.Select(MapLine).ToList();

        Row MapLine(string line)
        {
            var lineParts = line.Split(' ');
            
            var springs = lineParts[0];
            var checksums = lineParts[1].Split(',').Select(int.Parse).ToList();

            return new Row(springs, checksums);
        }
    }
    class Segment
    {
        public byte Counter { get; set; }
        public bool Closed { get; set; }
    }

    public static int One(string fileName = "Day12/input.txt")
    {
        var rows = ParseInput(fileName);

        var possibleArrangements = 0;
        
        foreach (var row in rows)
        {
            var unknownSpringIndexes = row.Springs
                .Select((s, i) => s == '?' ? i : (int?)null)
                .Where(x => x != null)
                .Select(x => x!.Value)
                .ToList();

            var rowPattern = row.Springs.ToCharArray();

            for (var i = 0; i < (int)Math.Pow(2, unknownSpringIndexes.Count); i++)
            {
                for (var j = 0; j < unknownSpringIndexes.Count; j++)
                {
                    var condition = (i & (int)Math.Pow(2, j)) == 0 ? '.' : '#';
                    
                    rowPattern[unknownSpringIndexes[j]] = condition;
                }

                if (CheckRowPattern(rowPattern, row.DamagedSpringsChecksums))
                {
                    possibleArrangements++;
                }
            }
        }
        
        Console.WriteLine("Day 12 Task 1 answer is: " + possibleArrangements);

        return possibleArrangements;

        
        bool CheckRowPattern(char[] rowPattern, IReadOnlyList<int> checksums)
        {
            var damagedCount = rowPattern.Count(x => x == '#');
            if (damagedCount != checksums.Sum())
            {
                return false;
            }
            
            var blocks = string.Join(string.Empty, rowPattern)
                .Split('.', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Length)
                .ToList();

            if (checksums.Count != blocks.Count)
            {
                return false;
            }
            
            for (var i = 0; i < checksums.Count; i++)
            {
                if (checksums[i] != blocks[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static int Two(string fileName = "Day12/input.txt")
    {
        Console.WriteLine("Day 12 Task 2 answer is: " + 1);

        return 1;
    }
}