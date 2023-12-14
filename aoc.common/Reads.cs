using System;
using System.Text.RegularExpressions;
using System.Linq;


namespace aoc.common
{
    public static class Reads
    {
        public static string[] ParseAsLines(this string input)
        {
            if (input.Contains(".txt"))//todo another way of dealing this?
                input = File.ReadAllText(input);
            var inputs = input.Split("\r\n");
            return inputs;
        }
        public static string[] ParseAsChunkOfLines(this string input)
        {
            if (input.Contains(".txt"))//todo another way of dealing this?
                input = File.ReadAllText(input);
            var inputs = input.Split("\r\n\r\n");
            return inputs;
        }

        public static string[] ReadTokens(string line, string splitPattern)
        {
            return Regex.Replace(line, splitPattern, " ").Trim().Split(' ');
        }

        public static long[] ReadNumbers(string line)
        {
            return ReadTokens(line, @"[^\-\d]+").Select(long.Parse).ToArray();
        }

        public static string[] ReadNonNumbers(string line)
        {
            return ReadTokens(line, @"[\d]+");
        }

        public static long ReadNumber(string line)
        {
            return ReadTokens(line, @"[^\-\d]+").Select(long.Parse).First();
        }

        public static long[][] ReadMatrixOfNumbers(string[] lines)
        {
            return lines.Select(ReadNumbers).ToArray();
        }

    }

}
