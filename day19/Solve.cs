using System.Text.RegularExpressions;

namespace day19;

public class Solve
{
    [Theory]
    [InlineData("sample.txt", true, 19114)]
    [InlineData("input.txt", false, 425811)]
    public void Part1(string input, bool isSample, int expected)
    {
        var parts = ParseParts(input);

        var sln1 = 0;
        foreach (var part in parts)
        {
            part.Dump("part");
            Runner s = isSample ? new SampleRunner() : new InputRunner();
            s.x = part["x"];
            s.m = part["m"];
            s.a = part["a"];
            s.s = part["s"];
            var res = s.in1();
            res.Dump("res");
            if (res ?? false)
                sln1 += s.x + s.m + s.a + s.s;
        }
        sln1.Dump("sln1").AssertSolved(expected);
    }

    [Theory]
    [InlineData("sample.txt", true, 19114)]
    [InlineData("input.txt", false, 131796824371749)]
    public void Part2(string input, bool isSample, long expected)
    {
        var workflows = input.ParseAsChunkOfLines()[0].ParseAsLines();

        var regex = new Regex("([xmas]{1,1})([<>]{1,1})([0-9]*)");//px{a<2006:qkq,m>2090:A,rfg}
        var conditions = workflows.SelectMany(workflow =>
        {
            var matches = regex.Matches(workflow);
            var part = matches.Select(match =>
            {
                var name = match.Groups[1].Value;
                var op = match.Groups[2].Value;
                var val = int.Parse(match.Groups[3].Value);
                return new { name, op, val };
            }).ToList();
            return part;
        }).ToList();

        var ranges = conditions.Select(condition =>
        {
            var name = condition.name;
            var op = condition.op;
            var val = condition.val;
            return (name, op == "<" ? val - 1 : val );
        }).ToArray();

        var sln2 = 0L;
        var prevx = 0;
        foreach (int x in ranges.Where(o => o.name == "x").Select(o => o.Item2).Concat(new int[] { 4000 }).Order())
        {
            x.Dump("x");
            var prevm = 0;
            foreach (int m in ranges.Where(o => o.name == "m").Select(o => o.Item2).Concat(new int[] { 4000 }).Order())
            {
                //m.Dump("m");
                var preva = 0;
                foreach (int a in ranges.Where(o => o.name == "a").Select(o => o.Item2).Concat(new int[] { 4000 }).Order())
                {
                    var prevs = 0;
                    foreach (int s in ranges.Where(o => o.name == "s").Select(o => o.Item2).Concat(new int[] { 4000 }).Order())
                    {
                        Runner runner = isSample ? new SampleRunner() : new InputRunner();
                        runner.x = x;
                        runner.m = m;
                        runner.a = a;
                        runner.s = s;
                        var res = runner.in1();
                        if (res ?? false)
                        {
                            sln2 += (x - prevx + 0L) * (m - prevm + 0L) * (a - preva + 0L) * (s - prevs + 0L);
                        }
                        prevs = s;
                    }
                    preva = a;
                }
                prevm = m;
            }

            prevx = x;
        }
        sln2.Dump("sln2").AssertSolved(expected);
    }

    private static List<Dictionary<string, int>> ParseParts(string input)
    {
        var partsv = input.ParseAsChunkOfLines()[1].ParseAsLines();
        var parts = partsv.Select(part =>
        {
            var vars = part[1..(part.Length - 1)].Split(",").Select(kv =>
            {
                var skv = kv.Split("=");
                return new { name = skv[0], val = int.Parse(skv[1]) };
            }).ToDictionary(x => x.name, x => x.val);
            return vars;
        }).ToList();
        return parts;
    }
    public Solve(ITestOutputHelper output)
    {
        this.Setup(output);
    }
}