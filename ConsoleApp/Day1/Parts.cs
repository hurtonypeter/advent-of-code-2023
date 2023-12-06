namespace ConsoleApp.Day1;

public static class Parts
{
    public static int One(string fileName = "Day1/input.txt")
    {
        var lines = File.ReadAllLines(fileName);

        var numbers = new List<int>();

        foreach (var line in lines)
        {
            int? n1 = null, n2 = null;
    
            foreach (var ch in line)
            {
                if (!char.IsDigit(ch)) continue;
                
                var number = int.Parse(ch.ToString());
        
                n1 ??= number;

                n2 = number;
            }
    
            numbers.Add(n1!.Value * 10 + n2!.Value);
        }

        var sum = numbers.Sum();
        
        Console.WriteLine("Day 1 Task 1 answer is: " + sum);

        return sum;
    }
    
    public static int Two(string fileName = "Day1/input.txt")
    {
        var lines = File.ReadAllLines(fileName);

        var numbers = new List<int>();
        string[] digits = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

        foreach (var line in lines)
        {
            int? n1 = null, n2 = null;
            var word = string.Empty;
    
            foreach (var ch in line)
            {
                if (char.IsDigit(ch))
                {
                    var number = int.Parse(ch.ToString());
        
                    n1 ??= number;

                    n2 = number;
                }
                else
                {
                    word += ch;
            
                    int FindDigitIndex(string str)
                    {
                        for (var index = 0; index < digits.Length; index++)
                        {
                            if (str.EndsWith(digits[index], StringComparison.InvariantCulture))
                            {
                                return index;
                            }
                        }

                        return -1;
                    }

                    var index = FindDigitIndex(word);
            
                    if (index > -1)
                    {
                        n1 ??= index + 1;

                        n2 = index + 1;
                    }
                }
            }
    
            numbers.Add(n1!.Value * 10 + n2!.Value);
        }

        var sum = numbers.Sum();
        
        Console.WriteLine("Day 1 Task 2 answer is: " + sum);

        return sum;
    }
}