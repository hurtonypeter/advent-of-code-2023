namespace ConsoleApp.Day13;

public static class Parts
{
    private record struct Coordinate(int X, int Y);

    private static List<List<string>> ParseInput(string fileName)
    {
        var input = File.ReadAllText(fileName);

        var blocks = input
            .Split(Environment.NewLine + Environment.NewLine)
            .Select(block => block.Split(Environment.NewLine).ToList())
            .ToList();

        return blocks;
    }

    private static int FindHorizontalReflection(List<string> lines, bool tryToFix = false)
    {
        var highestReflection = 0;

        for (var i = 1; i < lines.Count; i++)
        {
            var isReflection = true;
            for (int j = i - 1, k = i; j >= 0 && k < lines.Count; j--, k++)
            {
                if (lines[j] != lines[k])
                {
                    isReflection = false;
                    break;
                }
            }

            if (isReflection)
            {
                highestReflection = i;
                //break; // ?? 
            }
        }

        return highestReflection;
    }

    private static IEnumerable<string> Transform(List<string> input)
    {
        var length = input[0].Length;
        var chars = input.Select(x => x.ToCharArray()).ToList();

        for (var i = 0; i < length; i++)
        {
            var index = i;
            yield return string.Join(string.Empty, chars.Select(x => x[index]));
        }
    }

    private static int SummarizeBlocks(List<List<string>> blocks, bool tryToFix = false)
    {
        var sum = 0;

        foreach (var block in blocks)
        {
            var horizontalReflection = FindHorizontalReflection(block, tryToFix);

            sum += horizontalReflection * 100;

            var verticalReflection = FindHorizontalReflection(Transform(block).ToList(), tryToFix);

            sum += verticalReflection;
        }

        return sum;
    }

    public static int One(string fileName = "Day13/input.txt")
    {
        var blocks = ParseInput(fileName);

        var sum = SummarizeBlocks(blocks);

        Console.WriteLine("Day 13 Task 1 answer is: " + sum);

        return sum;
    }

    public static int Two(string fileName = "Day13/input.txt")
    {
        var blocks = ParseInput(fileName);

        var sum = SummarizeBlocks(blocks, true);
        
        Console.WriteLine("Day 13 Task 2 answer is: " + sum);

        return sum;
    }
}