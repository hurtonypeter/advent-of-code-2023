namespace ConsoleApp.Day1;

public static class Parts
{
    public static void One()
    {
        var lines = File.ReadAllLines("Day1/input.txt");

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

        Console.WriteLine("Day 1 Task 1 answer is: " + numbers.Sum());
    }
    
    public static void Two()
    {
        var lines = File.ReadAllLines("Day1/input.txt");

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
        
        Console.WriteLine("Day 1 Task 2 answer is: " + numbers.Sum());
    }
}