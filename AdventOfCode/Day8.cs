namespace AdventOfCode;
public class Day8
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("Input\\Day8\\day8_1.in");
        string instructions = lines[0];
        Dictionary<string, (string left, string right)> nodes = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(new[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length is not 2)
            {
                Console.WriteLine($"Invalid line format: {lines[i]}");
                continue;
            }
            string[] connections = parts[1].Trim('(', ')').Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (connections.Length != 2)
            {
                Console.WriteLine($"Invalid connections format: {parts[1]}");
                continue;
            }
            nodes[parts[0]] = (connections[0], connections[1]);
        }

        int steps = CountStepsToZZZ(nodes, instructions);
        Console.WriteLine($"Result: {steps}");
    }

    private static int CountStepsToZZZ(Dictionary<string, (string left, string right)> nodes, string instructions)
    {
        string currentNode = "AAA";
        int steps = 0;
        int instructionIndex = 0;

        while (currentNode is not "ZZZ")
        {
            (string left, string right) = nodes[currentNode];
            currentNode = instructions[instructionIndex] is 'L' ? left : right;
            steps++;
            instructionIndex = (instructionIndex + 1) % instructions.Length;
        }
        return steps;
    }
}