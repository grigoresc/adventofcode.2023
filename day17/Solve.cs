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

    void Part1(string input, int expected)
    {
        var map = input.ParseAsLines().Select(x => x.ToCharArray()).ToArray();
        map.Dump();
        var min1 = move(map, new Pos(0, 0), new Pos(map.Length - 1, map.Length - 1),'>').Dump();
        var min2 = move(map, new Pos(0, 0), new Pos(map.Length - 1, map.Length - 1),'V').Dump();

        var min = Math.Min(min1, min2);
        min.AssertSolved(expected);
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
   

    long move(char[][] map, Pos src, Pos dest, char currentDir)
    {
        //$"currentDir:{currentDir}".Dump();
        //map.Dump();
        if (src.x == dest.x && src.y == dest.y)
        {
            return 0;
        }
        //$"{src.x},{src.y}".Dump();
        var minCost = long.MaxValue;
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
                        var res = move(map, curPos, dest, dir);

                        cost += res;

                        if (cost < minCost)
                        {
                            minCost = cost;
                        }
                    }else
                    {
                        break;
                    }
                }else
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

        return minCost;
    }

    [Theory]
    [InlineData("sample.txt", 102)]
    void Part2(string input, long expected)
    {
    }

    public Solve(ITestOutputHelper output)
    {
        this.Setup(output);
    }
}