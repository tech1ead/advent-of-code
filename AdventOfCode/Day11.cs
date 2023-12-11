namespace AdventOfCode;
public class Day11
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("Input\\Day11\\day11_1.in");
        char[,] universe = ExpandUniverse(lines);
        List<(int, int)> galaxies = GetGalaxies(universe);
        int sum = CalculatePathSum(universe, galaxies);
        Console.WriteLine($"Result: {sum}");
    }

    static char[,] ExpandUniverse(string[] lines)
    {
        bool[] emptyRows = new bool[lines.Length];
        bool[] emptyCols = new bool[lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                if (lines[i][j] is '#')
                {
                    emptyRows[i] = true;
                    emptyCols[j] = true;
                }
            }
        }

        int newRowCount = emptyRows.Count(b => !b) * 2 + emptyRows.Count(b => b);
        int newColCount = emptyCols.Count(b => !b) * 2 + emptyCols.Count(b => b);
        char[,] expanded = new char[newRowCount, newColCount];

        int newRow = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            int newCol = 0;
            for (int j=0; j< lines[0].Length; j++)
            {
                expanded[newRow, newCol] = lines[i][j];
                if (!emptyCols[j])
                    expanded[newRow, ++newCol] = lines[i][j];
                newCol++;
            }
            if (!emptyRows[i])
                Array.Copy(expanded, newRow * newColCount, expanded, (++newRow) * newColCount, newColCount);
            newRow++;
        }
        return expanded;
    }

    static List<(int, int)> GetGalaxies(char[,] universe)
    {
        List<(int, int)> galaxies = new();
        for (int i = 0; i < universe.GetLength(0); i++)
        {
            for (int j=0; j < universe.GetLength(1); j++)
            {
                if (universe[i, j] is '#')
                    galaxies.Add((i, j));
            }
        }
        return galaxies;
    }

    static int CalculatePathSum(char[,] universe, List<(int, int)> galaxies)
    {
        int sum = 0;
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                sum += BFS(universe, galaxies[i], galaxies[j]);
            }
        }
        return sum;
    }

    static int BFS(char[,] grid, (int, int) start, (int, int) end)
    {
        Queue<((int, int), int)> queue = new();
        HashSet<(int, int)> visited = new();
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        queue.Enqueue((start, 0));
        visited.Add(start);

        while (queue.Count > 0)
        {
            ((int, int)current, int dist) = queue.Dequeue();
            if (current == end) return dist;

            for (int i = 0; i < 4; i++)
            {
                int nx = current.Item1 + dx[i];
                int ny = current.Item2 + dy[i];

                if (nx >= 0 && ny >= 0 && nx < grid.GetLength(0) && ny < grid.GetLength(1) && !visited.Contains((nx, ny)))
                {
                    visited.Add((nx, ny));
                    queue.Enqueue(((nx, ny), dist + 1));
                }
            }
        }
        return int.MaxValue;
    }
}