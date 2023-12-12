
//var lines = File.ReadAllLines("sample.txt");
var lines = File.ReadAllLines("input.txt");

var map = lines
    .Select(x => x.Split(" "))
    .Select(x => new
    {
        m = x[0],
        l = x[1].Split(",").Select(o => int.Parse(o))
    });

var cnt = 0;

foreach (var line in map)
    calc(line.m, line.l.ToArray());

void calc(string originalString, int[] lengths)
{
    generate(originalString, (originalString).ToCharArray(), 0, lengths);
}

Console.WriteLine(cnt);//7843
void generate(string originalString, char[] w, int position, int[] lengths)
{
    if(position== originalString.Length)
    {
        var cl = 0;
        var current = 0;
        for (var i = 0; i < position; i++)
        {
            var x = w[i];
            if (x == '#')
            {
                cl++;
            }
            else if (x == '.')
            {
                if (cl > 0)
                {
                    if (current == lengths.Length || cl != lengths[current])
                    {
                        return;
                    }
                    else
                    {
                        cl = 0;
                        current++;
                    }
                }
            }
        }

        if (cl > 0)
        {
            if (current == lengths.Length || cl != lengths[current])
            {
                return;
            }
            else
            {
                cl = 0;
                current++;
            }
        }

        if (position == originalString.Length)
        {
            if (cl == 0 && current == lengths.Length)
            {
                cnt++;
            }
        }
        return;
    }

    var c = originalString[position];
    if (c == '?')
    {
        var save = c;
        w[position] = '.';
        generate(originalString, w, position + 1, lengths);
        w[position] = '#';
        generate(originalString, w, position + 1, lengths);
        w[position] = c;
        return;
    }
    else
    {
        generate(originalString, w, position + 1, lengths);
    }
}

