
namespace day12;

public partial class Solve
{
    [Theory]
    [InlineData("sample.txt", 1, 21)]
    [InlineData("input.txt", 1, 7843)]
    [InlineData("?.### 1,3", 1, 1)]
    [InlineData("?###???????? 3,2,1", 1, 10)]

    [InlineData("sample.txt", 5, 525152)]
    [InlineData(".??..??...?##. 1,1,3", 5, 16384)]

    [InlineData("input.txt", 5, 525152)]
    //[InlineData("sample.txt", 21)]
    //[InlineData("input.txt", 7843)]
    //[InlineData("input.txt", 7843)]
    void Part2(string input, int repeat, int expected)
    {
        var map = Read(input);
        cnt = 0;
        foreach (var line in map)
        {
            //repeat a char[]
            var s = line.Springs.ToCharArray();
            for (var i = 0; i < repeat-1; i++)
            {
                s = s.Concat(new char[] { '?' }).ToArray();
                s = s.Concat(line.Springs.ToCharArray()).ToArray();
            }
            var l = new int[] { };
            for (var i = 0; i < repeat; i++)
            {
                l = l.Concat(line.Lengths.ToArray()).ToArray();
            }

            line.Springs.Dump("compute");
            calc2(
                s,
                l,
                "");
            cnt.Dump("cnt");
        }
        cnt.Dump().AssertSolved(expected);
    }

    private void calc2(char[] springs, int[] lengths, string sofar)
    {
        //"".Dump("-------------------");
        //springs.Dump("calc2");
        //lengths.Dump("lengths");

        //sofar.Dump("sofar");


        if (springs.Length > 0 && springs[0] == '.')
        {
            calc2(springs[1..], lengths, sofar + ".");
            return;
        }

        if (springs.Length == 0 && lengths.Length == 0)
        {
            //$"{sofar} zero".Dump();

            cnt++;

            return;
        }

        //if (lengths.Length == 0)
        //{
        //    return;
        //}
        //if is # then it's damaged; if it's . then it's not
        //var damagedString = lengths[0];
        //we build a list of # with length=len (where # is actualy a damaged spring)
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
                    calc2(springs[(i + 1)..], lengths, sofar + ".");
                }
                else
                {
                    if (lengths.Length > 0 && len == lengths[0])
                    {
                        calc2(springs[(i + 1)..], lengths[1..], sofar + $"(#{len}).");
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
                calc2(springs[(idx)..], new int[0], sofar + $"(#{len})N");
                //lengths[0].Dump("lengths[0]");
                //$"{sofar}(#{len}!)".Dump();
                //cnt++;
                //return;
            }
            else
                calc2(springs[idx..], lengths[1..], sofar + "G");
        }
        else
        {
            //do nothing
        }

    }
}

