using System.Text.Json;

var lines = File.ReadAllLines("input.txt");

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
                    if (str.EndsWith(digits[index]))
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


Console.WriteLine(JsonSerializer.Serialize(numbers));
Console.WriteLine(numbers.Sum());