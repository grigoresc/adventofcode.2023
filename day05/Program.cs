using aoc.common;
using Xunit;

//var input = File.ReadAllText("sample.txt");
var input = File.ReadAllText("input.txt");

var inputs = input.Split("\r\n\r\n");


//q:3 4 5 6
//read numbers
//var seeds = Reads.readNumbers(inputs[0]);
//
var seeds = Reads.ReadNumbers(inputs[0]);
var maps = inputs[1..].Select(r => r.Split("\r\n")[1..].Select(x => Reads.ReadNumbers(x)).ToArray()).ToArray();

var sln1 = seeds.Select(x => findPos(x)).Min();
Console.WriteLine($"sln1={sln1}");
//Assert.Equal(173706076, sln1);

[Fact]
void Solve1()
{
    var sln1 = seeds.Select(x => findPos(x)).Min();
    Console.WriteLine($"sln1={sln1}");
    Assert.Equal(173706076, sln1);

}

const int DEST = 0;
const int SRC = 1;
const int LEN = 2;
long findPos(long seed)
{
    long dest = seed;
    for (int m = 0; m < maps.Length; m++)
    {
        var map = maps[m];
        for (int i = 0; i < map.Length; i++)
        {
            var range = map[i];
            if (range[SRC] <= dest && dest < range[SRC] + range[LEN])
            {
                dest = range[DEST] + dest - range[SRC];
                break;
            }
        }
    }
    return dest;
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

        var intersect =  Segments.Intersect(Tuple.Create(src, src + srcrange), Tuple.Create(range[SRC], range[SRC] + range[LEN]));

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
[Fact]
void Solve2()
{

}
var sln2 = minLocation2;
Console.WriteLine($"sln2={sln2}");
Assert.Equal(11611182, sln2);

public partial class Program { }
