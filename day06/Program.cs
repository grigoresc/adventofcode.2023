

//var t = new int[] { 7, 15, 30 };
//var d = new int[] { 9, 40, 200 };
//var t = new long[] { 71530 };
//var d = new long[] { 940200 };


//var t = new int[] { 51, 92, 68, 90 };
//var d = new int[] { 222, 2031, 1126, 1225 };

var t = new long[] { 51926890 };
var d = new long[] { 222203111261225 };


var res = 1;
for (int i = 0; i < t.Length; i++)
{
    res *= FindWays(t[i], d[i]);
}
Console.WriteLine(res);//500346,42515755
int FindWays(long time, long distance)
{
    var cnt = 0;
    for (int i = 1; i < time; i++)
        if ((time - i) * i > distance)
            cnt++;
    return cnt;
}
