
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

var sln = seeds.Select(x => findPos(x)).Min();
Console.WriteLine($"min={sln}");



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
