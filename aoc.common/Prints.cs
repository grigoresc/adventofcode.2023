using System.Reflection;

namespace aoc.common
{
    public class Prints
    {
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
