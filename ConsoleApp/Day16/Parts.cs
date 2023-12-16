using System.Runtime.CompilerServices;

namespace ConsoleApp.Day16;

public static class Parts
{
    private enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    private record struct Coordinate(int X, int Y);

    private record struct Step(Coordinate Coordinate, Direction Direction);

    private static List<List<char>> ParseInput(string fileName)
    {
        var lines = File.ReadAllLines(fileName);

        return lines.Select(x => x.ToList()).ToList();
    }

    private static List<Step> GetNextSteps(Step step, List<List<char>> input)
    {
        var nextCellCoordinate = step.Direction switch
        {
            Direction.Up => step.Coordinate with {Y = step.Coordinate.Y - 1},
            Direction.Down => step.Coordinate with {Y = step.Coordinate.Y + 1},
            Direction.Right => step.Coordinate with {X = step.Coordinate.X + 1},
            Direction.Left => step.Coordinate with {X = step.Coordinate.X - 1},
            _ => throw new ArgumentException(step.Direction.ToString())
        };

        if (nextCellCoordinate.X < 0
            || nextCellCoordinate.Y < 0
            || nextCellCoordinate.X >= input[0].Count
            || nextCellCoordinate.Y >= input.Count)
        {
            return [];
        }

        var nextCell = input[nextCellCoordinate.Y][nextCellCoordinate.X];

        switch (nextCell)
        {
            case '.':
                return [step with {Coordinate = nextCellCoordinate}];

            case '|' when step.Direction is Direction.Up or Direction.Down:
                return [step with {Coordinate = nextCellCoordinate}];
            case '|':
                return [new Step(nextCellCoordinate, Direction.Up), new Step(nextCellCoordinate, Direction.Down)];

            case '-' when step.Direction is Direction.Right or Direction.Left:
                return [step with {Coordinate = nextCellCoordinate}];
            case '-':
                return [new Step(nextCellCoordinate, Direction.Right), new Step(nextCellCoordinate, Direction.Left)];

            case '/':
                return step.Direction switch
                {
                    Direction.Up => [new Step(nextCellCoordinate, Direction.Right)],
                    Direction.Down => [new Step(nextCellCoordinate, Direction.Left)],
                    Direction.Left => [new Step(nextCellCoordinate, Direction.Down)],
                    Direction.Right => [new Step(nextCellCoordinate, Direction.Up)],
                    _ => throw new ArgumentOutOfRangeException(step.Direction.ToString())
                };
            case '\\':
                return step.Direction switch
                {
                    Direction.Up => [new Step(nextCellCoordinate, Direction.Left)],
                    Direction.Down => [new Step(nextCellCoordinate, Direction.Right)],
                    Direction.Left => [new Step(nextCellCoordinate, Direction.Up)],
                    Direction.Right => [new Step(nextCellCoordinate, Direction.Down)],
                    _ => throw new ArgumentOutOfRangeException(step.Direction.ToString())
                };
            default:
                throw new ArgumentOutOfRangeException(nextCell.ToString());
        }
    }

    private static int GetEnergizedCountFromStartingStep(Step startingStep, List<List<char>> input)
    {
        var energized = new Dictionary<Step, bool>
        {
            {startingStep, true}
        };
        var beams = new List<Step>
        {
            startingStep
        };

        while (beams.Count > 0)
        {
            var nextBeams = new List<Step>();
            foreach (var beam in beams)
            {
                var nextSteps = GetNextSteps(beam, input);

                nextBeams.AddRange(nextSteps);
            }

            beams = [..nextBeams.Where(x => !energized.ContainsKey(x))];
            foreach (var beam in beams)
            {
                energized.TryAdd(beam, true);
            }
        }

        return energized.DistinctBy(x => x.Key.Coordinate).Count();
    }

    public static int One(string fileName = "Day16/input.txt")
    {
        var input = ParseInput(fileName);

        var count = GetEnergizedCountFromStartingStep(
            new Step(new Coordinate(0, 0), Direction.Right),
            input);

        Console.WriteLine("Day 16 Task 1 answer is: " + count);

        return count;
    }

    public static int Two(string fileName = "Day16/input.txt")
    {
        var input = ParseInput(fileName);

        var max = 0;
        for (var i = 0; i < input.Count; i++)
        {
            var countFromRight = GetEnergizedCountFromStartingStep(
                new Step(new Coordinate(0, i), Direction.Right),
                input);
            var countFromLeft = GetEnergizedCountFromStartingStep(
                new Step(new Coordinate(input[0].Count - 1, i), Direction.Left),
                input);
            
            max = Math.Max(max, Math.Max(countFromLeft, countFromRight));
        }
        
        for (var i = 0; i < input[0].Count; i++)
        {
            var countFromRight = GetEnergizedCountFromStartingStep(
                new Step(new Coordinate(i, 0), Direction.Down),
                input);
            var countFromLeft = GetEnergizedCountFromStartingStep(
                new Step(new Coordinate(i, input.Count - 1), Direction.Up),
                input);
            
            max = Math.Max(max, Math.Max(countFromLeft, countFromRight));
        }

        Console.WriteLine("Day 16 Task 2 answer is: " + max);

        return max;
    }
}