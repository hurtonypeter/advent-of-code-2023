namespace ConsoleApp.Day11;

public static class Parts
{
    private record struct Point(int X, int Y);

    private static List<Point> ParseInput(string fileName, int expansionAmount)
    {
        var lines = File.ReadAllLines(fileName).Select(x => x.ToCharArray()).ToArray();

        var points = new List<Point>();

        var expandedX = 0;
        var expandedY = 0;

        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].All(ch => ch == '.'))
            {
                expandedY += expansionAmount - 1;
                continue;
            }

            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines.All(x => x[j] == '.'))
                {
                    expandedX += expansionAmount - 1;
                    continue;
                }

                if (lines[i][j] != '.')
                {
                    points.Add(new Point(j + expandedX, i + expandedY));
                }
            }

            expandedX = 0;
        }

        return points;
    }

    private static ulong SumDistances(List<Point> points)
    {
        ulong sum = 0;

        for (var i = 0; i < points.Count - 1; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                sum += (ulong) Math.Abs(points[i].X - points[j].X) + (ulong) Math.Abs(points[i].Y - points[j].Y);
            }
        }

        return sum;
    }

    public static ulong One(string fileName = "Day11/input.txt")
    {
        var points = ParseInput(fileName, 2);

        var sum = SumDistances(points);

        Console.WriteLine("Day 11 Task 1 answer is: " + sum);

        return sum;
    }

    public static ulong Two(string fileName = "Day11/input.txt", int expansionAmount = 1_000_000)
    {
        var points = ParseInput(fileName, expansionAmount);

        var sum = SumDistances(points);

        Console.WriteLine("Day 11 Task 2 answer is: " + sum);

        return sum;
    }
}