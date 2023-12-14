namespace day12;

public partial class Solve
{
    int cnt = 0;
    [Theory]
    [InlineData("sample.txt", 21)]
    [InlineData("input.txt", 7843)]
    void Part1(string input, int expected)
    {
        var map = Read(input);
        cnt = 0;
        foreach (var line in map)
            calc(line.Springs, line.Lengths.ToArray());
        cnt.Dump().AssertSolved(expected);
    }

    void calc(string originalString, int[] lengths)
    {
        generate(originalString, originalString.ToCharArray(), 0, lengths);
    }

    void generate(string originalString, char[] w, int position, int[] lengths)
    {
        if (position == originalString.Length)
        {
            var cl = 0;
            var current = 0;
            for (var i = 0; i < position; i++)
            {
                var x = w[i];
                if (x == '#')
                {
                    cl++;
                }
                else if (x == '.')
                {
                    if (cl > 0)
                    {
                        if (current == lengths.Length || cl != lengths[current])
                        {
                            return;
                        }
                        else
                        {
                            cl = 0;
                            current++;
                        }
                    }
                }
            }

            if (cl > 0)
            {
                if (current == lengths.Length || cl != lengths[current])
                {
                    return;
                }
                else
                {
                    cl = 0;
                    current++;
                }
            }

            if (position == originalString.Length)
            {
                if (cl == 0 && current == lengths.Length)
                {
                    cnt++;
                }
            }
            return;
        }

        var c = originalString[position];
        if (c == '?')
        {
            var save = c;
            w[position] = '.';
            generate(originalString, w, position + 1, lengths);
            w[position] = '#';
            generate(originalString, w, position + 1, lengths);
            w[position] = c;
            return;
        }
        else
        {
            generate(originalString, w, position + 1, lengths);
        }
    }
    private static IEnumerable<Record> Read(string input)
    {
        var lines = input.ParseAsLines();

        var map = lines
            .Select(x => x.Split(" "))
            .Select(x => new Record(x[0], x[1].Split(",").Select(o => int.Parse(o))
            ));
        return map;
    }

    public Solve(ITestOutputHelper output)
    {
        this.Setup(output);
    }
}

internal record Record(string Springs, IEnumerable<int> Lengths);
