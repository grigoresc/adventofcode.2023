using System.Collections.Generic;
using System.Reflection;

namespace aoc.common
{
    public class Prints
    {
        public static void print<K,V>(Dictionary<K,V> dict, Action<string>? printer = null)
        {
            if (printer == null)
                printer = Console.WriteLine;
            foreach (var item in dict)
            {
                printer($"{item.Key}={item.Value}");
            }

        }
        public static void printItem<T>(T item, Action<string>? printer = null)
        {
            if (printer == null)
                printer = Console.WriteLine;
            printer(item.ToString());
        }

        public static void print<T>(T[] lst, Action<string>? printer = null)
        {
            if (printer == null)
                printer = Console.WriteLine;
            printer(String.Join(" ", lst));
        }
        public static void print<T1,T2>((T1,T2)[] lst, Action<string>? printer = null)
        {
            if (printer == null)
                printer = Console.WriteLine;
            printer(String.Join(" ", lst.Select(o=>$"{o.Item1}:{o.Item2}")));
        }
        public static void print<T>(T[][] lst, Action<string>? printer = null)
        {
            foreach (var item in lst)
                print(item, printer);
        }
        public static void print<T>(T[][][] lst, Action<string>? printer = null)
        {
            foreach (var item in lst)
            {
                print(item, printer);
                printItem("", printer);
            }
        }
    }
}
