namespace ConsoleApp.Day8;

public partial class Parts
{
    private static ulong FindLcmOfArray(IEnumerable<ulong> numbers)
    {
        var primeFactorsCount = new Dictionary<ulong, int>();

        foreach (var number in numbers)
        {
            var factors = PrimeFactors(number);

            foreach (var factor in factors)
            {
                var count = factors.Count(f => f == factor);
                if (!primeFactorsCount.ContainsKey(factor) || count > primeFactorsCount[factor])
                {
                    primeFactorsCount[factor] = count;
                }
            }
        }

        ulong lcm = 1;

        foreach (var kvp in primeFactorsCount)
        {
            lcm *= (ulong) Math.Pow(kvp.Key, kvp.Value);
        }

        return lcm;
    }

    private static List<ulong> PrimeFactors(ulong number)
    {
        var factors = new List<ulong>();
        ulong divisor = 2;

        while (number > 1)
        {
            while (number % divisor == 0)
            {
                factors.Add(divisor);
                number /= divisor;
            }

            divisor++;
        }

        return factors;
    }
}