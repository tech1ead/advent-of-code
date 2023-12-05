namespace AdventOfCode;
public class Day5
{
    public static void Main()
    {
        string[] input = File.ReadAllLines("Input\\Day5\\day5_1.in");
        List<long> seeds = ParseSeeds(input[0]);
        Dictionary<string, List<(long, long, long)>> mappings = ParseMappings(input.Skip(1));

        List<long> locations = seeds.Select(seed => MapThroughCategories(seed, mappings)).ToList();
        long lowestLocation = locations.Min();

        Console.WriteLine($"Result: {lowestLocation}");
    }

    private static List<long> ParseSeeds(string line)
    {
        return line.Substring(line.IndexOf(':') + 1)
                   .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(long.Parse)
                   .ToList();
    }

    private static Dictionary<string, List<(long, long, long)>> ParseMappings(IEnumerable<string> lines)
    {
        Dictionary<string, List<(long, long, long)>> mappings = new();
        string currentMapping = string.Empty;

        foreach (string line in lines)
        {
            if (line.Contains("map:"))
            {
                currentMapping = line.Split(':')[0];
                mappings[currentMapping] = new List<(long, long, long)>();
            }
            else
            {
                long[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(str => long.Parse(str))
                                .ToArray();

                if (parts.Length is 3)
                {
                    mappings[currentMapping].Add((parts[0], parts[1], parts[2]));
                }
            }
        }
        return mappings;
    }

    private static long MapThroughCategories(long seed, Dictionary<string, List<(long, long, long)>> mappings)
    {
        long currentNumber = seed;
        foreach (KeyValuePair<string, List<(long, long, long)>> mapping in mappings)
        {
            currentNumber = MapNumber(currentNumber, mapping.Value);
        }
        return currentNumber;
    }

    private static long MapNumber(long number, List<(long destStart, long sourceStart, long rangeLength)> mapping)
    {
        foreach (var (destStart, sourceStart, rangeLength) in mapping)
        {
            if (number >= sourceStart && number < sourceStart + rangeLength)
            {
                return destStart + (number - sourceStart);
            }
        }
        return number;
    }
}