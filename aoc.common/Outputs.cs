using Xunit;
namespace aoc.common;

public enum InputType
{
    File,
    Inline
}
public static class Outputs
{
    public static object Dump<T>(this T o)
    {
        Console.WriteLine(o);
        return o;
    }

    public static object AssertEqual<T>(this T o, T expected)
    {
        Assert.Equal(expected, o);
        return o;
    }
}

