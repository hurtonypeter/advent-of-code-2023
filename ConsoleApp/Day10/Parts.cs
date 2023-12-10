namespace ConsoleApp.Day10;

public static class Parts
{
    private record struct Point(int X, int Y);

    private class Matrix
    {
        public Dictionary<Point, char> Fields { get; init; } = default!;
        public int Width { get; init; }
        public int Height { get; init; }

        private static readonly char[] AttachesFromAbove = {'7', 'F', '|'};
        private static readonly char[] AttachesFromBelow = {'L', 'J', '|'};
        private static readonly char[] AttachesFromRight = {'7', 'J', '-'};
        private static readonly char[] AttachesFromLeft = {'F', 'L', '-'};

        public Point GetStartingPoint()
        {
            return Fields.Single(x => x.Value == 'S').Key;
        }

        public IEnumerable<Point> GetStartingDirections()
        {
            var startingPoint = GetStartingPoint();

            var surroundingPoints = new[]
            {
                startingPoint with {X = startingPoint.X + 1},
                startingPoint with {X = startingPoint.X - 1},
                startingPoint with {Y = startingPoint.Y + 1},
                startingPoint with {Y = startingPoint.Y - 1}
            };

            if (Fields.ContainsKey(surroundingPoints[0]) && AttachesFromRight.Contains(Fields[surroundingPoints[0]]))
                yield return surroundingPoints[0];

            if (Fields.ContainsKey(surroundingPoints[1]) && AttachesFromLeft.Contains(Fields[surroundingPoints[1]]))
                yield return surroundingPoints[1];

            if (Fields.ContainsKey(surroundingPoints[2]) && AttachesFromBelow.Contains(Fields[surroundingPoints[2]]))
                yield return surroundingPoints[2];

            if (Fields.ContainsKey(surroundingPoints[3]) && AttachesFromAbove.Contains(Fields[surroundingPoints[3]]))
                yield return surroundingPoints[3];
        }

        public Point[] GetNextPoints(Point point)
        {
            var currentPipe = Fields[point];

            return currentPipe switch
            {
                '-' => new[] {point with {X = point.X + 1}, point with {X = point.X - 1}},
                '7' => new[] {point with {X = point.X - 1}, point with {Y = point.Y + 1}},
                '|' => new[] {point with {Y = point.Y - 1}, point with {Y = point.Y + 1}},
                'J' => new[] {point with {X = point.X - 1}, point with {Y = point.Y - 1}},
                'L' => new[] {point with {X = point.X + 1}, point with {Y = point.Y - 1}},
                'F' => new[] {point with {X = point.X + 1}, point with {Y = point.Y + 1}},
                _ => throw new Exception("Unknown pipe type")
            };
        }
    }

    private static Matrix ParseInput(string fileName)
    {
        var lines = File.ReadAllLines(fileName);

        var fields = lines.Select((x, i) =>
                x.ToCharArray().Select((y, j) => new KeyValuePair<Point, char>(new Point(j, i), y)))
            .SelectMany(x => x)
            .ToDictionary(x => x.Key, x => x.Value);

        return new Matrix {Fields = fields, Height = lines.Length, Width = lines[0].Length};
    }

    private static (List<Point> path, int actionCounter) FindPath(Matrix matrix)
    {
        var startingPoint = matrix.GetStartingPoint();
        var currentPoints = matrix.GetStartingDirections().ToArray();
        if (currentPoints.Length != 2)
        {
            throw new Exception("Something is wrong");
        }

        var visitedPoints = new List<Point>(new[] {currentPoints[0], startingPoint, currentPoints[1]});
        var actionCounter = 1;

        while (currentPoints[0] != currentPoints[1])
        {
            var nextPoints1 = matrix.GetNextPoints(currentPoints[0]);
            var nextPoints2 = matrix.GetNextPoints(currentPoints[1]);
            var nextPoint1 = nextPoints1.Single(x => !visitedPoints.Contains(x));
            var nextPoint2 = nextPoints2.Single(x => !visitedPoints.Contains(x));

            currentPoints = new[] {nextPoint1, nextPoint2};
            visitedPoints.Insert(0, currentPoints[0]);
            visitedPoints.Add(currentPoints[1]);

            actionCounter++;
        }

        if (visitedPoints.First() == visitedPoints.Last())
        {
            visitedPoints.RemoveAt(visitedPoints.Count - 1);
        }

        return (visitedPoints, actionCounter);
    }

    private static bool IsPointInPolygon(Point point, List<Point> polygon)
    {
        var inside = false;

        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
                point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) +
                polygon[i].X)
            {
                inside = !inside;
            }
        }

        return inside;
    }

    private static List<Point> GetEnclosedPoints(List<Point> loop, int matrixWidth, int matrixHeight)
    {
        var enclosedPoints = new List<Point>();

        for (var x = 0; x < matrixWidth; x++)
        {
            for (var y = 0; y < matrixHeight; y++)
            {
                var currentPoint = new Point(x, y);

                if (IsPointInPolygon(currentPoint, loop))
                {
                    enclosedPoints.Add(currentPoint);
                }
            }
        }

        return enclosedPoints;
    }

    public static int One(string fileName = "Day10/input.txt")
    {
        var matrix = ParseInput(fileName);

        var (_, actionCounter) = FindPath(matrix);

        Console.WriteLine("Day 10 Task 1 answer is: " + actionCounter);

        return actionCounter;
    }


    public static int Two(string fileName = "Day10/input.txt")
    {
        var matrix = ParseInput(fileName);

        var (visitedPoints, _) = FindPath(matrix);

        var result = GetEnclosedPoints(visitedPoints, matrix.Width, matrix.Height);

        var enclosedPoints = result.FindAll(x => !visitedPoints.Contains(x));

        Console.WriteLine("Day 10 Task 2 answer is: " + enclosedPoints.Count);

        return enclosedPoints.Count;
    }
}