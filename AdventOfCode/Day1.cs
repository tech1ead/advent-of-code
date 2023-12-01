using System.Text.RegularExpressions;

namespace AdventOfCode;
public class Day1
{
    static string[] input1 = File.ReadAllLines("Input\\day1_1.in");
    static string[] input2 = File.ReadAllLines("Input\\day1_2.in");
    static Dictionary<string, int> dict = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };

    public static void Main()
    {

        // For the first half of the puzzle
        int resultSum1 = 0;
        foreach (string s in input1)
        {
            char firstChar = GetFirstCharDigit(s.ToCharArray());
            char secondChar = GetFirstCharDigit(s.ToCharArray().Reverse().ToArray());
            string res = new(new char[] { firstChar, secondChar });
            resultSum1 += int.Parse(res);
        }
        Console.WriteLine($"Result: {resultSum1}");


        // For the second half of the puzzle
        foreach(string s in input2)
        {
        }
        //Console.WriteLine($"Result: {resultSum2}");
    }
    // For the first half of the puzzle
    private static char GetFirstCharDigit(char[] chars)
    {
        foreach (char c in chars)
        {
            if (char.IsDigit(c))
            {
                return c;
            }
        }
        return '0';
    }
}