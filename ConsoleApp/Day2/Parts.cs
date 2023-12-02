using System.Text.RegularExpressions;

namespace ConsoleApp.Day2;

public static partial class Parts
{
    [GeneratedRegex("(\\d+) red")]
    private static partial Regex RedRegex();

    [GeneratedRegex("(\\d+) blue")]
    private static partial Regex BlueRegex();

    [GeneratedRegex("(\\d+) green")]
    private static partial Regex GreenRegex();

    private static readonly Regex RedMatcher = RedRegex();
    private static readonly Regex BlueMatcher = BlueRegex();
    private static readonly Regex GreenMatcher = GreenRegex();

    private record struct Game(int Id, List<Draw> Draws);

    private record struct Draw(int Red, int Green, int Blue);

    private static List<Game> ParseGames()
    {
        var lines = File.ReadAllLines("Day2/input.txt");

        return lines.Select(line =>
        {
            var meta = line.Split(':');

            var gameId = int.Parse(meta[0].Split(' ')[1]);

            var draws = meta[1].Split(';');

            return new Game(gameId, ParseDraws(draws).ToList());
        }).ToList();
    }

    private static IEnumerable<Draw> ParseDraws(IEnumerable<string> draws)
    {
        foreach (var draw in draws)
        {
            var redMatch = RedMatcher.Match(draw);
            var blueMatch = BlueMatcher.Match(draw);
            var greenMatch = GreenMatcher.Match(draw);

            var red = redMatch.Success ? int.Parse(redMatch.Groups[1].Value) : 0;
            var blue = blueMatch.Success ? int.Parse(blueMatch.Groups[1].Value) : 0;
            var green = greenMatch.Success ? int.Parse(greenMatch.Groups[1].Value) : 0;

            yield return new Draw(red, green, blue);
        }
    }

    public static void One()
    {
        const int reds = 12;
        const int greens = 13;
        const int blues = 14;

        var games = ParseGames();

        var possibleGames = games
            .Where(x =>
                x.Draws.All(d => d is {Red: <= reds, Green: <= greens, Blue: <= blues}))
            .ToList();

        Console.WriteLine("Day 2 Task 1 answer is: " + possibleGames.Sum(x => x.Id));
    }

    public static void Two()
    {
        var games = ParseGames();

        var sum = 0;

        foreach (var game in games)
        {
            var maxRed = game.Draws.Max(x => x.Red);
            var maxGreen = game.Draws.Max(x => x.Green);
            var maxBlue = game.Draws.Max(x => x.Blue);
            
            sum += maxRed * maxGreen * maxBlue;
        }
        
        Console.WriteLine("Day 2 Task 2 answer is: " + sum);
    }
}