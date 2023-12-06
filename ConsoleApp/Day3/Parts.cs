namespace ConsoleApp.Day3;

public static class Parts
{
    public record struct Coordinate(int X, int Y);

    public record struct HorizontalVector(int X1, int X2, int Y);

    private class Matrix
    {
        public readonly Dictionary<Coordinate, char> Symbols = new();
        public readonly Dictionary<HorizontalVector, int> Numbers = new();

        public Matrix(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            for (var i = 0; i < lines.Length; i++)
            {
                var chars = lines[i].AsSpan();
                var numberPartial = string.Empty;

                for (var j = 0; j < chars.Length; j++)
                {
                    var ch = chars[j];

                    if (char.IsDigit(ch))
                    {
                        numberPartial += ch;
                        continue;
                    }

                    if (numberPartial.Length > 0)
                    {
                        var number = int.Parse(numberPartial);
                        Numbers.Add(new HorizontalVector(j - numberPartial.Length, j - 1, i), number);
                        numberPartial = string.Empty;
                    }

                    if (ch != '.')
                    {
                        Symbols.Add(new Coordinate(j, i), ch);
                    }
                }

                if (numberPartial.Length > 0)
                {
                    var number = int.Parse(numberPartial);
                    Numbers.Add(
                        new HorizontalVector(chars.Length - 1 - numberPartial.Length, chars.Length - 1 - 1, i), number);
                }
            }
        }
    }

    public static bool IsCoordinateNextToHorizontalVector(Coordinate coordinate, HorizontalVector vector)
    {
        if (coordinate.Y < vector.Y - 1 || coordinate.Y > vector.Y + 1)
        {
            return false;
        }

        return coordinate.X >= vector.X1 - 1 && coordinate.X <= vector.X2 + 1;
    }

    public static int One(string fileName = "Day3/input.txt")
    {
        var matrix = new Matrix(fileName);
        var numbers = new List<int>();

        foreach (var number in matrix.Numbers)
        {
            if (matrix.Symbols.Any(x => IsCoordinateNextToHorizontalVector(x.Key, number.Key)))
            {
                numbers.Add(number.Value);
            }
        }

        var sum = numbers.Sum();

        Console.WriteLine("Day 3 Task 1 answer is: " + sum);

        return sum;
    }
    
    public static int Two(string fileName = "Day3/input.txt")
    {
        var matrix = new Matrix(fileName);
        var sum = 0;

        foreach (var stars in matrix.Symbols.Where(x => x.Value == '*'))
        {
            var numbersCloseToStar = matrix.Numbers
                .Where(x => IsCoordinateNextToHorizontalVector(stars.Key, x.Key))
                .ToList();
            
            if (numbersCloseToStar.Count == 2)
            {
                sum += numbersCloseToStar[0].Value * numbersCloseToStar[1].Value;
            }
        }

        Console.WriteLine("Day 3 Task 2 answer is: " + sum);

        return sum;
    }
}