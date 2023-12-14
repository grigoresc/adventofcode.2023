namespace day12;

public partial class Solve
{
    [Theory]
    [InlineData("sample.txt", 525152)]
    //[InlineData("input.txt", 7843)]
    void Part2(string input, int expected)
    {
        var map = Read(input);
        cnt = 0;
        foreach (var line in map)
            calc(line.Springs, line.Lengths.ToArray());
        cnt.Dump().AssertSolved(expected);
    }
}

