namespace AdventOfCode;
public class Day10
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("Input\\Day10\\day10_1.in");
        int width = lines[0].Length;
        int height = lines.Length;
        Exits[,] graph = new Exits[height, width];
        (int x, int y) entryPoint = ParseMap(lines, graph);

        SetEntryPointExits(graph, entryPoint, width, height);
        int maxDist = PerformFloodFill(graph, entryPoint, width, height);

        Console.WriteLine($"Result: {maxDist}");
    }

    private static (int x, int y) ParseMap(string[] lines, Exits[,] graph)
    {
        int width = lines[0].Length;
        int height = lines.Length;
        (int x, int y) entryPoint = (0, 0);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                graph[y, x] = ParseCharacter(lines[y][x]);
                if (lines[y][x] is 'S') entryPoint = (x, y);

                RemoveOffMapExits(graph, x, y, width, height);
            }
        }
        return entryPoint;
    }

    private static Exits ParseCharacter(char ch)
    {
        return ch switch
        {
            '|' => Exits.N | Exits.S,
            '-' => Exits.E | Exits.W,
            'L' => Exits.N | Exits.E,
            'J' => Exits.N | Exits.W,
            '7' => Exits.S | Exits.W,
            'F' => Exits.S | Exits.E,
            _ => Exits.None,
        };
    }

    private static void RemoveOffMapExits(Exits[,] graph, int x, int y, int width, int height)
    {
        if (x is 0) graph[y, x] &= ~Exits.W;
        if (x == width - 1) graph[y, x] &= ~Exits.E;
        if (y is 0) graph[y, x] &= ~Exits.N;
        if (y == height - 1) graph[y, x] &= ~Exits.S;
    }

    private static void SetEntryPointExits(Exits[,] graph, (int x, int y) entryPoint, int width, int height)
    {
        Exits[] oppositeDirections = { Exits.S, Exits.N, Exits.W, Exits.E };
        (int dx, int dy)[] neighborDirections = { (0, -1), (0, 1), (1, 0), (-1, 0) };

        for (int i = 0; i < 4; i++)
        {
            (int dx, int dy) = neighborDirections[i];
            int nx = entryPoint.x + dx;
            int ny = entryPoint.y + dy;
            if (nx >= 0 && nx < width && ny >= 0 && ny < height && (graph[ny, nx] & oppositeDirections[i]) is not Exits.None)
            {
                graph[entryPoint.y, entryPoint.x] |= (Exits)(1 << i);
            }
        }
    }

    private static int PerformFloodFill(Exits[,] graph, (int x, int y) entryPoint, int width, int height)
    {
        int maxDist = 0;
        int[,] distances = new int[height, width];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                distances[y, x] = int.MaxValue;

        distances[entryPoint.y, entryPoint.x] = 0;
        Queue<(int x, int y)> positions = new ();
        positions.Enqueue(entryPoint);

        while (positions.Count > 0)
        {
            (int x, int y) p = positions.Dequeue();
            maxDist = UpdateDistances(graph, distances, positions, p, maxDist, width, height);
        }

        return maxDist;
    }

    private static int UpdateDistances(Exits[,] graph, int[,] distances, Queue<(int x, int y)> positions, (int x, int y) p, int maxDist, int width, int height)
    {
        Exits exits = graph[p.y, p.x];
        int neighboringDist = distances[p.y, p.x] + 1;
        (int dx, int dy)[] neighborDirections = { (0, -1), (0, 1), (1, 0), (-1, 0) };

        for (int i = 0; i < 4; i++)
        {
            if ((exits & (Exits)(1 << i)) is not Exits.None)
            {
                (int dx, int dy) = neighborDirections[i];
                int nx = p.x + dx;
                int ny = p.y + dy;

                if (nx >= 0 && nx < width && ny >= 0 && ny < height && neighboringDist < distances[ny, nx])
                {
                    distances[ny, nx] = neighboringDist;
                    positions.Enqueue((nx, ny));
                    maxDist = Math.Max(maxDist, neighboringDist);
                }
            }
        }
        return maxDist;
    }
}
enum Exits
{
    None = 0,
    N = 1,
    S = 2,
    E = 4,
    W = 8
}