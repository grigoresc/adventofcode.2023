
//var input = File.ReadAllText("sample.txt");
var input = File.ReadAllText("input.txt");


static void print(long[] lst)
{
    Console.WriteLine(String.Join(" ", lst));
}
static void print2(long[][] lst)
{
    foreach (var item in lst)
        print(item);
}
static void print3(long[][][] lst)
{
    foreach (var item in lst)
    {
        print2(item);
        Console.WriteLine();
    }
}

var inputs = input.Split("\r\n\r\n");

var seeds = inputs[0].Split(" ")[1..].Select(x => long.Parse(x)).ToArray();
var maps = inputs[1..].Select(r => r.Split("\r\n")[1..].Select(x => x.Split(" ").Select(y => long.Parse(y)).ToArray()).ToArray()).ToArray();
print(seeds);
print3(maps);

foreach (var seed in seeds)
{
    show(seed);
}

var sln1 = seeds.Select(x => findPos(x)).Min();
Console.WriteLine($"sln1={sln1}");



void show(long seed)
{
    Console.WriteLine($"{seed} to {findPos(seed)}");
}

const int DEST = 0;
const int SRC = 1;
const int LEN = 2;

long findPos(long seed)
{
    long currentPos = -1;
    currentPos = seed;
    for (int m = 0; m < maps.Length; m++)
    {
        var map = maps[m];
        for (int i = 0; i < map.Length; i++)
        {
            var range = map[i];
            if (range[SRC] <= currentPos && currentPos < range[SRC] + range[LEN])
            {
                currentPos = range[DEST] + currentPos - range[SRC];
                break;
            }
        }
    }


    return currentPos;
}

Tuple<long, long>? Intersect(Tuple<long, long> segment1, Tuple<long, long> segment2)
{
    var (a, b) = segment1;
    var (c, d) = segment2;
    if (a > b)
        (a, b) = (b, a);
    if (c > d)
        (c, d) = (d, c);
    if (b < c || d < a)
        return null;
    return Tuple.Create(Math.Max(a, c), Math.Min(b, d));

}
var minLocation2 = 1000000000000000L;
void findPos2(long src, long srcrange, int idxMap)
{
    if (idxMap == maps.Length)
    {
        if (src < minLocation2)
            minLocation2 = src;
        return;
    }

    var map = maps[idxMap];

    var has = false;
    foreach (var range in map)
    {

        var intersect = Intersect(Tuple.Create(src, src + srcrange), Tuple.Create(range[SRC], range[SRC] + range[LEN]));

        if (intersect != null)
        {
            var dest = range[DEST];
            if (range[SRC] < intersect.Item1)
            {
                dest = range[DEST] + intersect.Item1 - range[SRC];
            }
            else if (range[SRC] > intersect.Item1)
            {
                dest = range[DEST] - range[SRC] + intersect.Item1;
            }

            findPos2(dest, intersect.Item2 - intersect.Item1, idxMap + 1);
            has = true;
        }
    }
    if (!has)
        findPos2(src, srcrange, idxMap + 1);
}

for (int i = 0; i < seeds.Length / 2; i++)
{
    findPos2(seeds[i * 2], seeds[i * 2 + 1], 0);
}

var sln2 = minLocation2;
Console.WriteLine($"sln2={sln2}");//11611182

