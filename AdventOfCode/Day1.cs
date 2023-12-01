using System.Text.RegularExpressions;

namespace AdventOfCode;
public class Day1
{
    static string[] input1 = File.ReadAllLines("Input\\day1_1.in");
    static string[] input2 = File.ReadAllLines("Input\\day1_2.in");
    static Dictionary<string, string> dict = new()
    {
          {"one","o1e"},
          {"two","t2o"},
          {"three","t3e"},
          {"four","f4r"},
          {"five","f5e"},
          {"six","s6x"},
          {"seven","s7n"},
          {"eight","e8t"},
          {"nine","n9e"}
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
        int resultSum2 = 0;
        foreach (string s in input2)
        {
            var stringToCalc = ReplaceStr(s);
            char firstChar = GetFirstCharDigit(stringToCalc.ToCharArray());
            char secondChar = GetFirstCharDigit(stringToCalc.ToCharArray().Reverse().ToArray());
            string res = new(new char[] { firstChar, secondChar });
            resultSum2 += int.Parse(res);
        }
        Console.WriteLine($"Result: {resultSum2}");
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
    private static string ReplaceStr(string s)
    {
        foreach (var pair in dict)
        {
            // Replace each occurrence of the word with its numeric value
            s = s.Replace(pair.Key, pair.Value.ToString());
        }
        return s;
    }

}