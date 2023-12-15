namespace ConsoleApp.Day15;

public static class Parts
{
    private static List<List<char>> ParseInput(string fileName)
    {
        var input = File.ReadAllText(fileName);
        var steps = input.Split(',').Select(x => x.ToList()).ToList();

        return steps;
    }

    private static int HashString(List<char> chars)
    {
        var result = 0;

        foreach (var c in chars)
        {
            result += c;
            result *= 17;
            result %= 256;
        }

        return result;
    }

    public static int One(string fileName = "Day15/input.txt")
    {
        var steps = ParseInput(fileName);

        var sum = steps.Sum(HashString);

        Console.WriteLine("Day 15 Task 1 answer is: " + sum);

        return sum;
    }

    public static int Two(string fileName = "Day15/input.txt")
    {
        var steps = ParseInput(fileName);

        var boxes = new Dictionary<int, List<(string Label, int FocalLength)>>();

        foreach (var step in steps)
        {
            var action = step.Contains('=') ? '=' : '-';
            var label = step[..step.IndexOf(action)];
            var focalLength = step[(step.IndexOf(action) + 1)..];

            var hash = HashString(label);
            var labelString = string.Join(string.Empty, label);

            if (action == '=')
            {
                if (!boxes.ContainsKey(hash))
                {
                    boxes.Add(hash, []);
                }

                if (boxes[hash].Exists(x => x.Label == labelString))
                {
                    var box = boxes[hash];
                    var index = box.FindIndex(x => x.Label == labelString);
                    box.RemoveAt(index);
                    box.Insert(index, (
                        labelString,
                        int.Parse(string.Join(string.Empty, focalLength))));
                }
                else
                {
                    boxes[hash].Add((
                        labelString,
                        int.Parse(string.Join(string.Empty, focalLength))));
                }
            }
            else
            {
                if (!boxes.ContainsKey(hash))
                {
                    continue;
                }

                boxes[hash].RemoveAll(x => x.Label == labelString);
            }
        }

        var sum = 0;
        foreach (var box in boxes.Where(x => x.Value.Count > 0))
        {
            for (var i = 0; i < box.Value.Count; i++)
            {
                sum += (box.Key + 1) * (i + 1) * box.Value[i].FocalLength;
            }
        }

        Console.WriteLine("Day 15 Task 2 answer is: " + sum);

        return sum;
    }
}