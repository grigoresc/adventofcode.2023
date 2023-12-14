//var lines = File.ReadAllLines("sample.txt");
var lines = File.ReadAllLines("input.txt");

var map = lines
    .Select(x => x.ToArray()).ToArray();

var len = map.Length;
MoveNorth(map);
var sln1 = map.Select((line, idx) => line.Where(c => c == 'O').Count() * (len - idx)).Sum();
Console.WriteLine(sln1);//102497

var cycles = 1000000000;

var cmap = map = lines
    .Select(x => x.ToArray()).ToArray();
var lst = new Dictionary<string, (int, int)>();
var rep = false;
var firstRepSize = 0;
var lastRepSize = 0;
var skipCalc = false;
var sln2 = 0;
for (int c = 0; c < cycles; c++)
{
    cmap = Cycle(cmap);
    sln2 = cmap.Select((line, idx) => line.Where(c => c == 'O').Count() * (len - idx)).Sum();

    var hash = cmap.Select(
        (line, idx) => line.Select(
            (c, jdx) => c.ToString() + idx.ToString() + jdx.ToString())
        ).SelectMany(x => x).Aggregate((x, y) => x + y);

    if (!skipCalc)
        if (lst.ContainsKey(hash))
        {
            if (!rep)
            {
                rep = true;
                firstRepSize = c;
                lst.Clear();
                lst.Add(hash, (c, sln2));
            }
            else
            {
                lastRepSize = c;
                c = (cycles / (lastRepSize - firstRepSize)) * (lastRepSize - firstRepSize);

                var repeatSize = lastRepSize - firstRepSize;
                var repeatCount = (cycles - firstRepSize) / repeatSize;
                var nextc = firstRepSize + (repeatCount * repeatSize);
                c = nextc;
                skipCalc = true;

            }
        }
        else
        {
            lst.Add(hash, (c, sln2));
        }

}

Console.WriteLine(sln2);//105008

//print map
void print(char[][] map, string? title = "")
{
    Console.WriteLine($"~~~~~~~~~~~~~~{title}~~~~~~~~~~~~~~");
    foreach (var line in map)
    {
        Console.WriteLine(new string(line));
    }

}

char[][] Cycle(char[][] map)
{
    MoveNorth(map);
    map = Rotate(map);
    MoveNorth(map);
    map = Rotate(map);
    MoveNorth(map);
    map = Rotate(map);
    MoveNorth(map);
    map = Rotate(map);
    return map;
}

char[][] Rotate(char[][] map)
{
    //rotate clockwise
    var newmap = new char[len][];
    for (int i = 0; i < len; i++)
    {
        newmap[i] = new char[len];
        for (int j = 0; j < len; j++)
        {
            newmap[i][j] = map[len - j - 1][i];
        }
    }
    return newmap;
}

void MoveNorth(char[][] map)
{
    for (int j = 0; j < len; j++)
    {
        var rocks = 0;
        var start = -1;
        for (int i = 0; i < len; i++)
        {
            if (map[i][j] != '#')
            {
                if (start == -1)
                {
                    start = i;
                }
            }
            if (map[i][j] == 'O')
            {
                rocks++;
            }
            else if (map[i][j] == '#')
            {
                if (start < i && start != -1)
                {
                    for (int r = 0; r < rocks; r++)
                    {
                        map[start + r][j] = 'O';
                    }
                    for (int r = start + rocks; r < i; r++)
                    {
                        if (map[r][j] == 'O')
                        {
                            map[r][j] = '.';
                        }
                    }
                }
                rocks = 0;
                start = -1;
            }

        }
        if (start != -1)
        {
            for (int r = 0; r < rocks; r++)
            {
                map[start + r][j] = 'O';
            }
            for (int r = start + rocks; r < len; r++)
            {
                if (map[r][j] == 'O')
                {
                    map[r][j] = '.';
                }
            }
        }
    }
}
