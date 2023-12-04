namespace AdventOfCode;
public class Day4
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("Input\\Day4\\day4_1.in");
        int totalPoints = lines.Sum(line => CalculatePoints(line));

        Console.WriteLine($"Result: {totalPoints}");
    }
    private static int CalculatePoints(string cardData)
    {
        string numericPart = cardData.Substring(cardData.IndexOf(':') + 1).Trim();

        string[] parts = numericPart.Split('|');
        HashSet<int> winningNumbers = parts[0].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        IEnumerable<int> yourNumbers = parts[1].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

        int points = 0;
        bool isFirstMatch = true;

        foreach (int number in yourNumbers.Distinct())
        {
            if (winningNumbers.Contains(number))
            {
                if (isFirstMatch)
                {
                    points += 1;
                    isFirstMatch = false;
                }
                else
                {
                    points *= 2;
                }
            }
        }
        return points;
    }
}