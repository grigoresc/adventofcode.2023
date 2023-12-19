using Xunit;
using Xunit.Abstractions;
namespace aoc.common;
public static class Outputs
{
    static ITestOutputHelper output;
    public static void Setup<T>(this T o, ITestOutputHelper output)
    {
        Outputs.output = output;
    }

    static Action<string> printer
    {
        get
        {
            if (output != null)
                return output.WriteLine;
            else
                return Console.WriteLine;
        }
    }

    public static void Dump(this char[][] map, string? title = "")
    {
        Prints.printItem($"~~~~~~~~~~~~~~{title}~~~~~~~~~~~~~~", printer);
        foreach (var line in map)
        {
            Prints.printItem(new string(line), printer);
        }
    }

    public static object Dump<K,V>(this Dictionary<K,V> dict, string? expl = null)
    {
        if (!string.IsNullOrEmpty(expl))
            Prints.printItem($"{expl}", printer);
        Prints.print(dict, printer);

        return dict;
    }

    public static object Dump<T>(this T[][] o, string? expl = null)
    {
        if (!string.IsNullOrEmpty(expl))
            Prints.printItem($"{expl}", printer);
        Prints.print(o, printer);

        return o;
    }

    public static object Dump<T>(this T[][][] o, string? expl = null)
    {
        if (!string.IsNullOrEmpty(expl))
            Prints.printItem($"{expl}", printer);
        Prints.print(o, printer);
        return o;
    }

    public static T[] Dump<T>(this T[] o, string? expl = null)
    {
        if (!string.IsNullOrEmpty(expl))
            Prints.printItem($"{expl}", printer);
        Prints.print(o, printer);
        return o;
    }

    public static T Dump<T>(this T o, string? expl = null)
    {
        if (!string.IsNullOrEmpty(expl))
            Prints.printItem($"{expl} {o}", printer);
        else
        {
            Prints.printItem(o, printer);
        }
        return o;
    }

    public static T AssertSolved<T>(this T o, T expected)
    {
        Assert.Equal(expected, o);
        return o;
    }
}

