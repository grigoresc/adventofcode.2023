namespace aoc.common
{
    public class Segments
    {
        public static Tuple<long, long>? Intersect(Tuple<long, long> segment1, Tuple<long, long> segment2)
        {
            var (a, b) = segment1;
            var (c, d) = segment2;
            if (a > b)
                (a, b) = (b, a);
            if (c > d)
                (c, d) = (d, c);
            if (b < c || d < a)
                return null;
            return Tuple.Create(Math.Max(a, c), Math.Min(b, d));
        }
    }
}
