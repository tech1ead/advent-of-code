namespace AdventOfCode;
public class Day3
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("Input\\Day3\\day3_1.in");
        int sum = 0;

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (char.IsDigit(lines[y][x]))
                {
                    string numberStr = ExtractNumber(lines[y], ref x);
                    int number = int.Parse(numberStr);

                    for (int i = 0; i < numberStr.Length; i++)
                    {
                        if (IsAdjacentToSymbol(lines, x - numberStr.Length + i, y))
                        {
                            sum += number;
                            break;
                        }
                    }
                }
            }
        }
        Console.WriteLine($"Result: {sum}");
    }

    private static string ExtractNumber(string line, ref int index)
    {
        string numberStr = "";
        while (index < line.Length && char.IsDigit(line[index]))
        {
            numberStr += line[index];
            index++;
        }
        return numberStr;
    }

    private static bool IsAdjacentToSymbol(string[] lines, int x, int y)
    {
        int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

        for (int i = 0; i < 8; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx >= 0 && nx < lines[y].Length && ny >= 0 && ny < lines.Length)
            {
                char c = lines[ny][nx];
                if (c is not '.' && !char.IsDigit(c))
                {
                    return true;
                }
            }
        }
        return false;
    }
}