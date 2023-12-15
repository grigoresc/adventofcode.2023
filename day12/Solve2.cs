
namespace day12;

public partial class Solve
{
    [Theory]
    [InlineData("sample.txt", 1, 21L)]
    [InlineData("input.txt", 1, 7843L)]
    [InlineData("?.### 1,3", 1, 1L)]
    [InlineData("?###???????? 3,2,1", 1, 10L)]

    [InlineData("sample.txt", 5, 525152L)]
    [InlineData(".??..??...?##. 1,1,3", 5, 16384L)]

    [InlineData("input.txt", 5, 10153896718999L)]
    void Part2(string input, int repeat, long expected)
    {
        var map = Read(input);
        var sln2 = 0L;
        foreach (var line in map)
        {
            //repeat a char[]
            var s = line.Springs.ToCharArray();
            for (var i = 0; i < repeat - 1; i++)
            {
                s = s.Concat(new char[] { '?' }).ToArray();
                s = s.Concat(line.Springs.ToCharArray()).ToArray();
            }
            var l = new int[] { };
            for (var i = 0; i < repeat; i++)
            {
                l = l.Concat(line.Lengths.ToArray()).ToArray();
            }

            sln2 += calc2(
                s,
                l,
                "");
        }
        sln2.Dump().AssertSolved(expected);
    }

    string key(char[] springs, int[] lengths)
    {
        return $"{new string(springs)}|{string.Join(",", lengths)}";
    }

    //memoize calc2
    private Dictionary<string, long> memo = new();

    private long calc2(char[] springs, int[] lengths, string sofar)
    {
        var hash = key(springs, lengths);
        if (memo.ContainsKey(hash))
        {
            return memo[hash];
        }
        
        var total = 0L;

        if (springs.Length > 0 && springs[0] == '.')
        {
            total += calc2(springs[1..], lengths, sofar + ".");
            return total;
        }

        if (springs.Length == 0 && lengths.Length == 0)
        {
            total++;
            return total;
        }

        var damagedStrignStarted = false;
        var len = 0;

        //find the next sure thing (not damaged one)
        var idxO = springs.Select((c, idx) => new { c, idx }).Where(c => c.c == '.').FirstOrDefault();

        var idx = 1;
        if (idxO == null)
        {
            idx = springs.Length;
        }
        else
        {
            idx = idxO.idx;
        }

        for (var i = 0; i < idx; i++)
        {
            var c = springs[i];
            if (c == '#')
            {
                if (damagedStrignStarted)
                {
                    len++;
                }
                else
                {
                    damagedStrignStarted = true;
                    len = 1;
                }
            }
            else if (c == '?')
            {
                //1. compute as if we had a . here
                if (!damagedStrignStarted)
                {
                    total += calc2(springs[(i + 1)..], lengths, sofar + ".");
                }
                else
                {
                    if (lengths.Length > 0 && len == lengths[0])
                    {
                        total += calc2(springs[(i + 1)..], lengths[1..], sofar + $"(#{len}).");
                    }
                    else
                    {
                        //do nothing
                    }
                }
                //2. compute as if we had a # here - meaning we go further
                len++;
                damagedStrignStarted = true;

            }
            else if (c == '.')
                throw new Exception("This should not happen");
        }

        if (len > 0 && lengths.Length > 0 && len == lengths[0])
        {
            if (lengths.Length == 1)
            {
                total += calc2(springs[(idx)..], new int[0], sofar + $"(#{len})N");
            }
            else
                total += calc2(springs[idx..], lengths[1..], sofar + "G");
        }
        else
        {
            //do nothing
        }

        memo[hash] = total;

        return total;
    }
}

