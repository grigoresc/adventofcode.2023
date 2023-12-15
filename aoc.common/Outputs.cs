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
    
    public static void Dump(this char[][] map, string? title = "")
    {
        if (output != null)
        {
            output.WriteLine($"~~~~~~~~~~~~~~{title}~~~~~~~~~~~~~~");
            foreach (var line in map)
            {
                output.WriteLine(new string(line));
            }
        }
    }

    public static object Dump<T>(this T[][] o, string? expl = null)
    {
        if (output != null)
        {
            if (!string.IsNullOrEmpty(expl))
                Prints.printItem($"{expl}", output.WriteLine);
            Prints.print(o, output.WriteLine);
        }

        return o;
    }

    public static object Dump<T>(this T[][][] o, string? expl = null)
    {
        if (output != null)
        {
            if (!string.IsNullOrEmpty(expl))
                Prints.printItem($"{expl}", output.WriteLine);
            Prints.print(o, output.WriteLine);
        }

        return o;
    }
    public static T[] Dump<T>(this T[] o, string? expl = null)
    {
        if (output != null)
        {
            if (!string.IsNullOrEmpty(expl))
                Prints.printItem($"{expl}", output.WriteLine);
            Prints.print(o, output.WriteLine);
        }
        return o;
    }

    public static T Dump<T>(this T o, string? expl = null)
    {
        Console.WriteLine(o);

        if (output != null)
        {
            if (!string.IsNullOrEmpty(expl))
                Prints.printItem($"{expl} {o}", output.WriteLine);
            else
            {
                Prints.printItem(o, output.WriteLine);
            }
        }
        return o;
    }

    public static T AssertSolved<T>(this T o, T expected)
    {
        Assert.Equal(expected, o);
        return o;
    }
}

