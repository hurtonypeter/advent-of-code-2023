namespace ConsoleApp.Day14;

public static class Parts
{
    private static IEnumerable<List<char>> ParseInput(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        
        var length = lines[0].Length;
        var chars = lines.Select(x => x.ToCharArray()).ToList();

        for (var i = 0; i < length; i++)
        {
            var index = i;
            yield return chars.Select(x => x[index]).ToList();
        }
    }
    
    private static int FindFirstEmptySpotFrom(List<char> chars, int fromIndex)
    {
        var index = fromIndex;
        for (var i = fromIndex - 1; i >= 0; i--)
        {
            if (chars[i] == '.')
            {
                index--;
            }
            else if (chars[i] == '#' || chars[i] == 'O')
            {
                break;
            }
        }

        return index;
    }

    public static int One(string fileName = "Day14/input.txt")
    {
        var cols = ParseInput(fileName).ToList();

        foreach (var col in cols)
        {
            for (var i = 1; i < col.Count; i++)
            {
                if (col[i] != 'O')
                {
                    continue;
                }
                
                var emptyIndex = FindFirstEmptySpotFrom(col, i);
                if (emptyIndex >= i)
                {
                    continue;
                }
                
                col[emptyIndex] = col[i];
                col[i] = '.';
            }
        }

        var sum = 0;
        foreach (var col in cols)
        {
            for (var i = 0; i < col.Count; i++)
            {
                if (col[i] == 'O')
                {
                    sum += col.Count - i;
                }
            }
        }
        
        Console.WriteLine("Day 14 Task 1 answer is: " + sum);

        return sum;
    }

    public static int Two(string fileName = "Day14/input.txt")
    {
        Console.WriteLine("Day 14 Task 2 answer is: " + 1);

        return 1;
    }
}