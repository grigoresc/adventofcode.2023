namespace day05;


public class Solve
{

    [Theory]
    [InlineData(InputType.File, "sample.txt", 35L)]
    [InlineData(InputType.File, "input.txt", 173706076L)]
    void Part1<T> (InputType type, string input,T expected)
    {
        var (seeds, maps) = Parse(type, input);

        seeds
            .Select(x => findPos(x, maps)).Min()
            .Dump()
            .AssertEqual(expected);
    }

    [Theory]
    [InlineData(InputType.File, "sample.txt", 46L)]
    [InlineData(InputType.File, "input.txt", 11611182L)]
    void Part2<T>(InputType type, string input, T expected)
    {
        var (seeds, maps) = Parse(type, input);

        var minLocation = 1000000000000000L;

        for (int i = 0; i < seeds.Length / 2; i++)
        {
            findPos2(seeds[i * 2], seeds[i * 2 + 1], 0, maps, ref minLocation);
        }
        minLocation
            .Dump()
            .AssertEqual(expected);
    }

    (long[], long[][][]) Parse(InputType type, string input)
    {
        if(type == InputType.File)
            input = File.ReadAllText(input);
        var inputs = input.Split("\r\n\r\n");

        var seeds = Reads.ReadNumbers(inputs[0]);
        var maps = inputs[1..].Select(r => r.Split("\r\n")[1..].Select(x => Reads.ReadNumbers(x)).ToArray()).ToArray();
        return (seeds, maps);
    }

    const int DEST = 0;
    const int SRC = 1;
    const int LEN = 2;
    long findPos(long seed, long[][][]? maps)
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

    void findPos2(long src, long srcrange, int idxMap, long[][][]? maps, ref long minLocation2)
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

            var intersect = Segments.Intersect(Tuple.Create(src, src + srcrange), Tuple.Create(range[SRC], range[SRC] + range[LEN]));

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

                findPos2(dest, intersect.Item2 - intersect.Item1, idxMap + 1, maps, ref minLocation2);
                has = true;
            }
        }
        if (!has)
            findPos2(src, srcrange, idxMap + 1, maps, ref minLocation2);
    }

}

