using System.Runtime.CompilerServices;

namespace day17;

public class Solve
{
    [Theory]
    [InlineData("sample.txt", 102)]
    [InlineData(
        "241\r\n" +
        "367\r\n" +
        "312", 2)]

    public void Part1(string input, int expected)
    {
        var map = input.ParseAsLines().Select(x => x.ToCharArray()).ToArray();
        map.Dump("Initial");
        var min1 = move(map, new Pos(0, 0), new Pos(map.Length - 1, map.Length - 1), 'V', 0).Dump();
        var min2 = move(map, new Pos(0, 0), new Pos(map.Length - 1, map.Length - 1), '>', 0).Dump();

        //var min = Math.Min(min1, min2);
        //min.AssertSolved(expected);
        min1.Dump("min1");
        min2.Dump("min2");
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
    }

    //Pos[] dirs = new Pos[] { new Pos(0, 1), new Pos(1, 0), new Pos(0, -1), new Pos(-1, 0) };

    char[] d = new char[] { '>', 'V', '<', '^' };


    Dictionary<char, char[]> NextDir = new Dictionary<char, char[]>
    {
        { '>', new char[] { 'V', '^' } },
        { 'V', new char[] { '<', '>' } },
        { '<', new char[] { '^', 'V' } },
        { '^', new char[] { '>', '<' } },
    };

    Pos NextPostByDir(char dir, Pos pos)
    {
        switch (dir)
        {
            case '>':
                return new Pos(pos.x, pos.y + 1);
            case 'V':
                return new Pos(pos.x + 1, pos.y);
            case '<':
                return new Pos(pos.x, pos.y - 1);
            case '^':
                return new Pos(pos.x - 1, pos.y);
            default:
                throw new Exception();
        }
    }



    Dictionary<string, long?> m = new();
    string hash(char[][] map, Pos src, char currentDir)
    {
        return string.Join($"{src.x}-{src.y}", map.Select(x => new string(x)))
            .Replace("<", ".").Replace(">", ".")
            .Replace("^", ".").Replace("V", ".");// + currentDir;
    }
    long? move(char[][] map, Pos src, Pos dest, char currentDir, int level)
    {
        var currentHash = hash(map, src, currentDir);
        if (m.ContainsKey(currentHash))
        {
            $"{currentHash}".Dump();
            Console.ReadLine();
            return m[currentHash];
        }

        //$"currentDir:{currentDir}".Dump();
        //map.Dump($"level {level} arrived at {src.x},{src.y} ({map[src.x][src.y]})");
        if (src.x == dest.x && src.y == dest.y)
        {
            m[currentHash] = 0;
            return 0;
        }
        //$"{src.x},{src.y}".Dump();
        long? minCost = null;
        foreach (var dir in NextDir[currentDir])
        {
            var curPos = src;
            var temp = new Dictionary<Pos, char>();
            for (int steps = 1; steps <= 3; steps++)
            {
                curPos = NextPostByDir(dir, curPos);
                if (curPos.x >= 0 && curPos.x < map.Length && curPos.y >= 0 && curPos.y < map.Length)
                {
                    //if (map[curPos.x][curPos.y] != '.')
                    if (!d.Contains(map[curPos.x][curPos.y]))
                    {
                        var cost = (long)(map[curPos.x][curPos.y] - '0');
                        //temp.Add(dir, map[src.x][src.y]);
                        temp[curPos] = map[curPos.x][curPos.y];
                        map[curPos.x][curPos.y] = dir;
                        //map[curPos.x][curPos.y] = d[Array.IndexOf(dirs, dir)];
                        var res = move(map, curPos, dest, dir, level + 1);

                        if (res != null)
                        {
                            cost += res.Value;

                            if (!minCost.HasValue || cost < minCost.Value)
                            {
                                minCost = cost;
                            }

                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            //restore temp
            foreach (var item in temp)
            {
                map[item.Key.x][item.Key.y] = item.Value;
            }
        }

        m[currentHash] = minCost;
        return minCost;
    }

    [Theory]
    [InlineData("sample.txt", 102)]
    public void Part2(string input, long expected)
    {
    }

    public Solve(ITestOutputHelper output)
    {
        this.Setup(output);
    }
}