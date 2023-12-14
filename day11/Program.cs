
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("sample.txt");
//var lines = File.ReadAllLines("input.txt");

var map = lines
    .Select(x => x.Split(" "))
    .Select(x => new
    {
        m = x[0],
        l = x[1].Split(",").Select(o => int.Parse(o))
    });

var sln2 = map.Select(x => new Solve2().calc2(x.m, x.l.ToArray())).Sum();

//void calc(string originalString, int[] lengths)
//{
//    generate(originalString, (originalString).ToCharArray(), 0, lengths);
//}
Console.WriteLine(sln2);//7843

class Solve2
{
    string originlString;
    int[] lengths;
    int cnt = 0;
    public int calc2(string originalString, int[] lengths)
    {
        this.originlString = originalString;
        this.lengths = lengths;
        generate2(0, 0);
        return cnt;
    }
    int generate2(int startPosition, int idxOfRemainingLenghts)
    {
        int reqLength = lengths[idxOfRemainingLenghts];
        //find left most # island
        int cpos = startPosition;
        while(cpos < originlString.Length && originlString[cpos] == '.')
        {
            cpos++;
        }



        cnt++;
        //?#?#?#?#?#?#?#? 1,3,1,6

        //if . generate2
        //if # we continue
        //if ? 1. we generate with . (as stop) 
        //if ? 2. we generate with # (as continue)

        var pos = startPosition;
        var total = 0;
        if (originlString[pos] == '.')
        {
            total += generate2(pos + 1, idxOfRemainingLenghts);
        }
        else
        {
            var sz = 0;
            while (originlString[pos+sz] == '#')
                sz++;
            if (pos == originlString.Length)
            {

            }

            if (originlString[pos] == '.')
            {
                generate2(pos + 1, idxOfRemainingLenghts + 1);
            }
        }

        return 0;
    }
}

//void generate(string originalString, char[] w, int position, int[] lengths)
//{
//    if(position== originalString.Length)
//    {
//        var cl = 0;
//        var current = 0;
//        for (var i = 0; i < position; i++)
//        {
//            var x = w[i];
//            if (x == '#')
//            {
//                cl++;
//            }
//            else if (x == '.')
//            {
//                if (cl > 0)
//                {
//                    if (current == lengths.Length || cl != lengths[current])
//                    {
//                        return;
//                    }
//                    else
//                    {
//                        cl = 0;
//                        current++;
//                    }
//                }
//            }
//        }

//        if (cl > 0)
//        {
//            if (current == lengths.Length || cl != lengths[current])
//            {
//                return;
//            }
//            else
//            {
//                cl = 0;
//                current++;
//            }
//        }

//        if (position == originalString.Length)
//        {
//            if (cl == 0 && current == lengths.Length)
//            {
//                cnt++;
//            }
//        }
//        return;
//    }

//    var c = originalString[position];
//    if (c == '?')
//    {
//        var save = c;
//        w[position] = '.';
//        generate(originalString, w, position + 1, lengths);
//        w[position] = '#';
//        generate(originalString, w, position + 1, lengths);
//        w[position] = c;
//        return;
//    }
//    else
//    {
//        generate(originalString, w, position + 1, lengths);
//    }
//}

