namespace AdventOfCode;
public class Day2
{
    public static void Main()
    {
        string[] input = File.ReadAllLines("Input\\Day2\\day2_1.in");

        int sumOfPossibleGameIds = CalculateSumOfPossibleGameIds(input);
        Console.WriteLine("Sum of IDs of possible games: " + sumOfPossibleGameIds);
    }
    static int CalculateSumOfPossibleGameIds(string[] input)
    {
        int sum = 0;
        foreach (var line in input)
        {
            if (IsGamePossible(line))
            {
                int gameId = ExtractGameId(line);
                sum += gameId;
            }
        }
        return sum;
    }

    static bool IsGamePossible(string gameData)
    {
        int maxRed = 12, maxGreen = 13, maxBlue = 14;
        string[] rounds = gameData.Split(';');
        foreach (string round in rounds)
        {
            int red = 0, green = 0, blue = 0;
            string[] cubes = round.Split(',');

            foreach (string cube in cubes)
            {
                string[] parts = cube.Trim().Split(' ');
                if (parts.Length is not 2) continue;

                int count = int.Parse(parts[0]);
                switch (parts[1])
                {
                    case "red":
                        red += count;
                        break;
                    case "green":
                        green += count;
                        break;
                    case "blue":
                        blue += count;
                        break;
                }
            }

            if (red > maxRed || green > maxGreen || blue > maxBlue) return false;
        }
        return true;
    }

    static int ExtractGameId(string gameData)
    {
        string[] parts = gameData.Split(':');
        return int.Parse(parts[0].Split(' ')[1]);
    }
}
