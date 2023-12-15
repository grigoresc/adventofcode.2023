namespace day15;

public class Solve
{
    [Theory]
    [InlineData("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", 1320)]
    [InlineData("input.txt", 517965)]
    void Part1(string input, int expected)
    {
        hash("HASH").Dump().AssertSolved(52);
        var cmds = input.ParseAsLines()[0].Split(",");
        cmds.Select(c => hash(c))
            .Sum()
            .Dump()
            .AssertSolved(expected);
    }
    int hash(string input)
    {
        var h = 0;
        foreach (var c in input)
        {
            h += c;
            h *= 17;
            h %= 256;
        }
        return h;
    }

    [Theory]
    [InlineData("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", 145)]
    [InlineData("input.txt", 267372L)]
    void Part2(string input, long expected)
    {
        var cmds = input.ParseAsLines()[0].Split(",");
        List<KeyValuePair<string, long>>[] slots = new List<KeyValuePair<string, long>>[256];
        for (int i = 0; i < 256; i++)
        {
            slots[i] = new List<KeyValuePair<string, long>>();
        }
        foreach (var cmd in cmds)
        {
            if (cmd.EndsWith("-"))
            {
                var s = cmd.Split("-");
                var key = s[0];
                var h = hash(key);
                var idx = slots[h].FindIndex(h => h.Key == key);
                if (idx >= 0)
                    slots[h].RemoveAt(idx);
            }
            else
            {
                var s = cmd.Split("=");
                var key = s[0];
                var value = int.Parse(s[1]);
                var h = hash(key);
                var idx = slots[h].FindIndex(h => h.Key == key);
                if (idx >= 0)
                {
                    slots[h][idx] = new KeyValuePair<string, long>(key, value);
                }
                else
                {
                    slots[h].Add(new KeyValuePair<string, long>(key, value));
                }
            }
        }

        slots
            .Select((s, idx) => s.Select((item, idx2) => item.Value * (idx + 1) * (idx2 + 1)).Sum())
            .Sum()
            .Dump()
            .AssertSolved(expected);
    }

    public Solve(ITestOutputHelper output)
    {
        this.Setup(output);
    }
}