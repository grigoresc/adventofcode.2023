namespace aoc.common
{
    public class Prints
    {
        public static void print<T> (T[] lst)
        {
            Console.WriteLine(String.Join(" ", lst));
        }
        public static void print<T>(T[][] lst)
        {
            foreach (var item in lst)
                print(item);
        }
        public static void print<T>(T[][][] lst)
        {
            foreach (var item in lst)
            {
                print(item);
                Console.WriteLine();
            }
        }
    }
}
