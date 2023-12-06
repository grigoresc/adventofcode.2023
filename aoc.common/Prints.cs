using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc.common
{
    public class Prints
    {
        public static void print(long[] lst)
        {
            Console.WriteLine(String.Join(" ", lst));
        }
        public static void print(long[][] lst)
        {
            foreach (var item in lst)
                print(item);
        }
        public static void print(long[][][] lst)
        {
            foreach (var item in lst)
            {
                print(item);
                Console.WriteLine();
            }
        }

    }
}
