using System.Text.RegularExpressions;

namespace AdventOfCode;
public class Day2
{
    public static void Main()
    {
        // First half
        string[] input1 = File.ReadAllLines("Input\\Day2\\day2_1.in");
        int sumOfPossibleGameIds = CalculateSumOfPossibleGameIds(input1);
        Console.WriteLine("Sum of IDs of possible games: " + sumOfPossibleGameIds);

        // Second half
        string[] input2 = File.ReadAllLines("Input\\Day2\\day2_2.in");
        List<CubeGame> games = ParseGames(input2);
        int powerSums = games.Select(game => CalculateMinimumPower(game)).Sum();
        Console.WriteLine($"Sum of powers: {powerSums}");
    }

    // For the first half
    private static int CalculateSumOfPossibleGameIds(string[] input)
    {
        int sum = 0;
        foreach (string line in input)
        {
            if (IsGamePossible(line))
            {
                int gameId = ExtractGameId(line);
                sum += gameId;
            }
        }
        return sum;
    }

    private static bool IsGamePossible(string gameData)
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
                if (parts.Length < 2) continue;

                if (int.TryParse(parts[0], out int count))
                {
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
            }

            if (red > maxRed || green > maxGreen || blue > maxBlue) return false;
        }
        return true;
    }

    private static int ExtractGameId(string gameData)
    {
        string[] parts = gameData.Split(':');
        return int.Parse(parts[0].Split(' ')[1]);
    }

    // For the second half
    private static List<CubeGame> ParseGames(string[] data)
    {
        List<CubeGame> games = new();
        Regex colorPattern = new (@"(\d+) (red|green|blue)");

        foreach (string line in data)
        {
            string[] parts = line.Split(':');
            CubeGame game = new () { Id = int.Parse(parts[0].Split(' ')[1]) };

            string[] sets = parts[1].Split(';');
            foreach (string set in sets)
            {
                MatchCollection matches = colorPattern.Matches(set);
                int red = 0, green = 0, blue = 0;
                foreach (Match match in matches.Cast<Match>())
                {
                    int count = int.Parse(match.Groups[1].Value);
                    switch (match.Groups[2].Value)
                    {
                        case "red": red += count; break;
                        case "green": green += count; break;
                        case "blue": blue += count; break;
                    }
                }
                game.Sets.Add((red, green, blue));
            }
            games.Add(game);
        }
        return games;
    }

    private static int CalculateMinimumPower(CubeGame game)
    {
        int maxRed = 0, maxGreen = 0, maxBlue = 0;
        foreach ((int Red, int Green, int Blue) in game.Sets)
        {
            maxRed = Math.Max(maxRed, Red);
            maxGreen = Math.Max(maxGreen, Green);
            maxBlue = Math.Max(maxBlue, Blue);
        }
        return maxRed * maxGreen * maxBlue;
    }
}

public class CubeGame
{
    public int Id { get; set; }
    public List<(int Red, int Green, int Blue)> Sets { get; set; } = new List<(int Red, int Green, int Blue)>();
}