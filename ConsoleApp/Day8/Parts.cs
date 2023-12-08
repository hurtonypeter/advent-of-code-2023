namespace ConsoleApp.Day8;

public static partial class Parts
{
    private record struct Node(string Key, string LeftDirection, string RightDirection);

    private static (char[] Instructions, Dictionary<string, Node> Nodes) ParseFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);

        var instructions = lines[0].ToCharArray();

        var nodes = new Dictionary<string, Node>();
        for (var i = 2; i < lines.Length; i++)
        {
            var instructionData = new Node(lines[i][..3], lines[i][7..10], lines[i][12..15]);
            nodes.Add(instructionData.Key, instructionData);
        }

        return (instructions, nodes);
    }

    public static ulong One(string fileName = "Day8/input.txt")
    {
        var (instructions, nodes) = ParseFile(fileName);

        var currentNode = nodes["AAA"];
        ulong actionCounter = 0;

        while (currentNode.Key != "ZZZ")
        {
            var action = instructions[actionCounter % (ulong) instructions.Length];

            currentNode = action switch
            {
                'L' => nodes[currentNode.LeftDirection],
                'R' => nodes[currentNode.RightDirection],
                _ => throw new Exception("Unknown action")
            };

            actionCounter++;
        }

        Console.WriteLine("Day 8 Task 1 answer is: " + actionCounter);

        return actionCounter;
    }

    public static ulong Two(string fileName = "Day8/input.txt")
    {
        var (instructions, nodes) = ParseFile(fileName);

        var currentNodes = nodes
            .Where(x => x.Key.EndsWith('A'))
            .Select(x => x.Value)
            .ToArray();

        var actionCounters = new List<ulong>();

        for (var i = 0; i < currentNodes.Length; i++)
        {
            var currentNode = currentNodes[i];
            ulong actionCounter = 0;
            while (currentNode.Key[2] != 'Z')
            {
                var action = instructions[actionCounter % (ulong) instructions.Length];

                currentNode = action switch
                {
                    'L' => nodes[currentNode.LeftDirection],
                    'R' => nodes[currentNode.RightDirection],
                    _ => throw new Exception("Unknown action")
                };

                actionCounter++;
            }

            actionCounters.Add(actionCounter);
        }

        var lcm = FindLcmOfArray(actionCounters);

        Console.WriteLine("Day 8 Task 2 answer is: " + lcm);

        return lcm;
    }
}