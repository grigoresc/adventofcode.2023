using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

//var lines = File.ReadAllLines("sample2.txt");
var lines = File.ReadAllLines("input.txt");

var instructions = lines[0];
var regex = new Regex(@"\((\w+), (\w+)\)");
(string, string) Parse(string s)
{
    var match = regex.Match(s);
    var groups = match.Groups;
    var key = groups[1].Value;
    var value = groups[2].Value;
    return (key, value);
}

var map = lines[2..].Select(x => x.Split(" = ")).ToDictionary(x => x[0], x => Parse(x[1]));

var cnt = 0;
var pos = "AAA";
while (pos != "ZZZ")
{
    var idx = cnt % instructions.Length;
    if (instructions[idx] == 'L')
        pos = map[pos].Item1;
    else
        pos = map[pos].Item2;
    cnt++;
}
var sln1 = cnt;
Console.WriteLine(sln1);//18727

var endWithA = map.Where(map => map.Key.EndsWith('A')).Select(map => map.Key).ToList();
var cnts = new List<int>();

foreach (var start in endWithA)
{
    pos = start;
    cnt = 0;
    while (true)
    {
        var idx = cnt % instructions.Length;
        if (instructions[idx] == 'L')
            pos = map[pos].Item1;
        else
            pos = map[pos].Item2;

        cnt++;

        if (pos.EndsWith("Z"))
        {
            cnts.Add(cnt);
            break;
        }
    }
}

BigInteger sln2 = 1;
for(int i = 0; i < cnts.Count; i++)
{
    sln2 = (sln2 * cnts[i]) / BigInteger.GreatestCommonDivisor(sln2, cnts[i]);
}

Console.WriteLine($"{sln2}");//18024643846273
