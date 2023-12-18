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
        var margins = new HashSet<PosRange>();
        var minPos = pos;
        var maxPos = pos;
        foreach (var item in map)
        {
            //for (var i = 0; i < item.Value; i++)
            //{
            //handle fixed Y
            //var nextPos = NextPosByDir(item.Key, pos, item.Value);
            if (item.Key == 'U' || item.Key == 'D')
            {
                var inc = item.Key == 'U' ? -1 : 1;
                var digsCount = item.Value;
                while (digsCount > 0)
                {
                    pos = NextPosByDir(item.Key, pos, 1);
                    
                    //find any intermmediate margins
                    foreach(var margin in margins)
                    {
                        if (margin.Pos.x == pos.x)
                        {
                            if (margin.Pos.y >= pos.y && margin.Pos.y <= pos.y + digsCount)
                            {
                                //found a margin
                                var len = Math.Min(margin.LenY, (ulong)(pos.y + digsCount - margin.Pos.y));
                                if (len > 0)
                                {
                                    //for (var y = pos.y; y != pos.y + len; y += inc)
                                    margins.Add(new PosRange(new Pos(pos.x, pos.y), 1, len));
                                    digsCount -= (int)len;
                                }
                            }
                        }
                    }
                }
                //for(long i = 0; i < item.Value; i++)
                //{

                //}
                //for (var x = pos.x; x != pos.x + item.Value; x += inc)
                    margins.Add(new PosRange(new Pos(pos.x, pos.y), 1, 1));
                //for(var y=pos.y+1;y!=pos.y+item.Value;y+=inc)
                    //margins.Add(new PosRange(new Pos(pos.x, y), 1, 1));
            }
            if (item.Value >= 1)
            {

            }
            margins.Add(new PosRange(pos, 1, 1));
            minPos = new Pos(Math.Min(minPos.x, pos.x), Math.Min(minPos.y, pos.y));
            maxPos = new Pos(Math.Max(maxPos.x, pos.x), Math.Max(maxPos.y, pos.y));
            //}
        }

        minPos.DumpS("minPos");
        maxPos.DumpS("maxPos");

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
            FillDraw(minPos, draw, point.Pos, '#');
        }

        //todo change here maybe..
        var startPos = new Pos(0, 3);//lucky guess from the draw
        FillDraw(minPos, draw, startPos, 'X');
        draw.Dump("init");

        fill(margins, filled, startPos, minPos, maxPos);

        foreach (var point in filled)
        {
            FillDraw(minPos, draw, point, ' ');
        }
        draw.Dump("filled");

        var m = margins.Count().Dump("m");
        var f = filled.Count().Dump("f");
        var sln = m + f;
        sln.Dump("sln");
        return sln;
    }

    private static void FillDraw(Pos minPos, char[][] draw, Pos point, char c)
    {
        //$"fill {point.x},{point.y} as {point.x - minPos.x},{point.y - minPos.y}".Dump();
        draw[point.x - minPos.x][point.y - minPos.y] = c;
    }

    private void fill(HashSet<PosRange> margins, HashSet<Pos> filled, Pos pos, Pos minPos, Pos maxPos)
    {
        if (margins.Contains(new PosRange(pos, 1, 1)))//todo change this..
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
                var nextPos = NextPosByDir(dir, cur, 1);
                if (
                    nextPos.x >= minPos.x
                    && nextPos.y >= minPos.y
                    && nextPos.x <= maxPos.x
                    && nextPos.y <= maxPos.y)
                    //todo - change here
                    if (!margins.Contains(new PosRange(nextPos, 1, 1)) && !filled.Contains(nextPos))
                    {
                        filled.Add(nextPos);
                        q.Enqueue(nextPos);
                    }
            }
        }
    }
    struct PosRange
    {
        public Pos Pos;
        public ulong LenX = 1;
        public ulong LenY = 1;
        public PosRange(Pos pos, ulong lenX, ulong lenY)
        {
            this.Pos = pos;
            this.LenX = lenX;
            this.LenY = lenY;
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

    Pos NextPosByDir(char dir, Pos pos, int len)
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