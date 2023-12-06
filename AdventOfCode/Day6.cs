namespace AdventOfCode;
public class Day6
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("Input\\Day6\\day6_1.in");

        int[] times = lines[0].Split(new[] { "Time:", " " }, StringSplitOptions.RemoveEmptyEntries)
                              .Select(int.Parse).ToArray();
        int[] distances = lines[1].Split(new[] { "Distance:", " " }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(int.Parse).ToArray();

        long totalWays = 1;

        for (int i = 0; i < times.Length; i++)
        {
            int waysToWin = CalculateWaysToWin(times[i], distances[i]);
            totalWays *= waysToWin;
        }

        Console.WriteLine($"Result: {totalWays}");
    }

    static int CalculateWaysToWin(int time, int recordDistance)
    {
        int ways = 0;
        for (int holdTime = 0; holdTime < time; holdTime++)
        {
            int travelTime = time - holdTime;
            int distance = holdTime * travelTime;
            if (distance > recordDistance)
            {
                ways++;
            }
        }
        return ways;
    }
}