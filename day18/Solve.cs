namespace day18;

public class Solve
{
    [Theory]
    [InlineData("sample.txt", 62)]
    [InlineData("input.txt", 46334)]
    public void Part1(string input, int expected)
    {
        var map = input.ParseAsLines()
            .Select(l => new KeyValuePair<char, int>(l[0], int.Parse(l[2..].Split(" ")[0])))
            .ToList();

        var sln1 = Dig(map);
        sln1.AssertSolved(expected);
    }

    [Theory]
    [InlineData("sample.txt", 952408144115L)]
    //[InlineData("input.txt", 952408144115L)]
    public void Part2(string input, long expected)
    {
        //R 6 (#70c710)=> R 461937

        KeyValuePair<char, int> Parse(string input)
        {
            var x = input.IndexOf('#');
            var hex = input.Substring(x + 1, 5);
            var dec = Convert.ToInt32(hex, 16);

            var d = input.Substring(x + 6, 1);
            if (d == "0")
                return new KeyValuePair<char, int>('R', dec);
            else if (d == "1")
                return new KeyValuePair<char, int>('D', dec);
            else if (d == "2")
                return new KeyValuePair<char, int>('L', dec);
            else if (d == "3")
                return new KeyValuePair<char, int>('U', dec);
            else
                throw new Exception("?");
        }

        var map = input.ParseAsLines()
            .Select(l => (Parse(l)))
            .ToList();

        var sln2 = Dig(map);
        sln2.AssertSolved(expected);
    }

    long Dig(List<KeyValuePair<char, int>> map)
    {
        var pos = new Pos(1, 1);
        var margins = new HashSet<Pos>();
        var minPos = pos;
        var maxPos = pos;
        foreach (var item in map)
        {
            for (var i = 0; i < item.Value; i++)
            {
                pos = NextPostByDir(item.Key, pos, 1);
                margins.Add(pos);
                minPos = new Pos(Math.Min(minPos.x, pos.x), Math.Min(minPos.y, pos.y));
                maxPos = new Pos(Math.Max(maxPos.x, pos.x), Math.Max(maxPos.y, pos.y));
            }
        }

        minPos.DumpS("minPos");
        maxPos.DumpS("maxPos");


        List<HashSet<Pos>> regions = new();
        var filled = new HashSet<Pos>();

        var draw = new char[maxPos.x - minPos.x + 1][];
        for (var i = 0; i < draw.Length; i++)
        {
            draw[i] = new char[maxPos.y - minPos.y + 1];
            for (var j = 0; j < draw[i].Length; j++)
            {
                draw[i][j] = '.';
            }
        }
        foreach (var point in margins)
        {
            FillPos(minPos, draw, point, '#');
        }
        
        var startPos = new Pos(0, 3);//lucky guess from the draw
        FillPos(minPos, draw, startPos, 'X');
        draw.Dump("init");
        
        fill(margins, filled, startPos, minPos, maxPos);

        foreach (var point in filled)
        {
            FillPos(minPos, draw, point, ' ');
        }
        draw.Dump("filled");

        var m = margins.Count().Dump("m");
        var f = filled.Count().Dump("f");
        var sln = m + f;
        sln.Dump("sln");
        return sln;
    }

    private static void FillPos(Pos minPos, char[][] draw, Pos point, char c)
    {
        //$"fill {point.x},{point.y} as {point.x - minPos.x},{point.y - minPos.y}".Dump();
        draw[point.x - minPos.x][point.y - minPos.y] = c;
    }

    private void fill(HashSet<Pos> margins, HashSet<Pos> filled, Pos pos, Pos minPos, Pos maxPos)
    {
        if (margins.Contains(pos))
        {
            throw new Exception("pos on a margin");
        }
        Queue<Pos> q = new();
        q.Enqueue(pos);
        while (q.Count() > 0)
        {
            var cur = q.Dequeue();
            foreach (var dir in "ULDR")
            {
                var nextPos = NextPostByDir(dir, cur, 1);
                if (
                    nextPos.x >= minPos.x
                    && nextPos.y >= minPos.y
                    && nextPos.x <= maxPos.x
                    && nextPos.y <= maxPos.y)
                    if (!margins.Contains(nextPos) && !filled.Contains(nextPos))
                    {
                        filled.Add(nextPos);
                        q.Enqueue(nextPos);
                    }
            }
        }
    }

    struct Pos
    {
        public int x;
        public int y;
        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Pos DumpS(string expl = null)
        {
            $"{this.x},{this.y}".Dump(expl);
            return this;
        }
    }

    Pos NextPostByDir(char dir, Pos pos, int len)
    {
        switch (dir)
        {
            case 'R':
                return new Pos(pos.x, pos.y + len);
            case 'U':
                return new Pos(pos.x + len, pos.y);
            case 'L':
                return new Pos(pos.x, pos.y - len);
            case 'D':
                return new Pos(pos.x - len, pos.y);
            default:
                throw new Exception();
        }
    }

    public Solve(ITestOutputHelper output)
    {
        this.Setup(output);
    }
}