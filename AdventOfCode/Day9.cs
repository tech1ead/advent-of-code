namespace AdventOfCode;
public class Day9
{
    public static void Main()
    {
        List<List<int>> input = ReadAndParseFile("Input\\Day9\\day9_1.in");

        long totalSum = 0;
        while (input.Any(seq => seq.Count > 0))
        {
            totalSum += input.Sum(seq => seq.LastOrDefault());
            input = input.Select(seq => CalculateDifferences(seq)).ToList();
        }
        Console.WriteLine($"Result: {totalSum}");
    }

    private static List<List<int>> ReadAndParseFile(string filePath)
    {
        return File.ReadAllLines(filePath)
                   .Select(line => line.Split(' ').Select(int.Parse).ToList())
                   .ToList();
    }

    private static List<int> CalculateDifferences(List<int> sequence)
    {
        if (sequence.Count < 2) return new List<int>();

        List<int> differences = new();
        for (int i = 0; i < sequence.Count - 1; i++)
        {
            differences.Add(sequence[i + 1] - sequence[i]);
        }
        return differences;
    }
}