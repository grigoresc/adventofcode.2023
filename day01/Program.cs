
internal class Program
{
    private static void Main(string[] args)
    {
        var x = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

        x = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

        var input = File.ReadAllText("input.txt");
        x = input;

        var lines = x.Split("\r\n");

        var spell = new Dictionary<string, char>() {
    {"one",'1'},{"two",'2'},{"three",'3'},{"four",'4'},{"five",'5'},
    {"six",'6'},{"seven",'7'},{"eight",'8'},{"nine",'9'},
    {"1",'1'},{ "2",'2'},{"3",'3'},{"4",'4'},{"5",'5'},
    {"6",'6'},{"7",'7'},{"8",'8'},{"9",'9'}};
        int find(string line)
        {
            var f = line.First(c => '0' <= c && c <= '9');
            var l = line.Last(c => '0' <= c && c <= '9');
            return int.Parse(f.ToString() + l.ToString());
        }
        int find2(string line)
        {
            var f = (from s in spell
                     let idxFirst = line.IndexOf(s.Key)
                     where idxFirst != -1
                     orderby idxFirst
                     select s.Value).First();
            var l = (from s in spell
                     let idxLast = line.LastIndexOf(s.Key)
                     where idxLast != -1
                     orderby idxLast descending
                     select s.Value).First();

            var ret = int.Parse(f.ToString() + l.ToString());
            return ret;
        }

        var ret = lines.Select(l => find(l)).Sum();//53080
        var ret2 = lines.Select(l => find2(l)).Sum();
        Console.WriteLine(ret);//53268
    }
}