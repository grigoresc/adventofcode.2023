//var lines = File.ReadAllText("sample.txt");
var lines = File.ReadAllLines("input.txt");

var hands = lines
    .Select(x => x.Split(" "))
    .Select((x, i) => new Hand
    {
        Card = x[0],
        Bid = int.Parse(x[1]),
        Pos = i
    })
    .ToList();

SortHands(hands, HandRank, Rank);
var sln1 = hands.Select((h, i) => h.Bid * (i + 1)).Sum();
Console.WriteLine(sln1);//251927063

SortHands(hands, HandRank2, Rank2);
var sln2 = hands.Select((h, i) => h.Bid * (i + 1)).Sum();
Console.WriteLine(sln2);//255632664

List<string> ReplaceJokers(string hand)
{
    var genHands = new List<string>();
    if (hand.Contains('J'))
        for (int j = 0; j < hand.Length; j++)
            if (hand[j] == 'J')
            {
                var cards = hand.Where(c => c != 'J').Distinct();

                foreach (var card in cards)
                    if (card != 'J')
                        genHands.Add(hand.Substring(0, j) + card + hand.Substring(j + 1));
            }
    var retHands = new List<string>();
    if (genHands.Count > 0)
    {
        foreach (var card in genHands)
        {
            retHands.AddRange(ReplaceJokers(card));
        }
    }
    else
        retHands.Add(hand);
    return retHands;
}

void SortHands(List<Hand> hands, Func<string, int> handRank, Func<char, int> cardRank)
{
    hands.Sort((a, b) =>
    {
        var aHandRank = handRank(a.Card);
        var bHandRank = handRank(b.Card);

        if (aHandRank != bHandRank)
            return aHandRank - bHandRank;

        for (int i = 0; i < a.Card.Length; i++)
        {
            var aRank = cardRank(a.Card[i]);
            var bRank = cardRank(b.Card[i]);
            if (aRank != bRank)
                return aRank - bRank;
        }
        return 0;
    });
}


int HandRank(string hand)
{
    //count pairs
    var pairs = 0;
    var counts = new int[15];
    for (int i = 0; i < hand.Length; i++)
    {
        counts[Rank(hand[i])]++;
    }
    for (int i = 0; i < counts.Length; i++)
    {
        if (counts[i] == 2)
            pairs++;
    }
    //count triples
    var triples = 0;
    for (int i = 0; i < counts.Length; i++)
    {
        if (counts[i] == 3)
            triples++;
    }
    //count quads
    var quads = 0;
    for (int i = 0; i < counts.Length; i++)
    {
        if (counts[i] == 4)
            quads++;
    }
    //count flush
    var flush = true;
    for (int i = 1; i < hand.Length; i++)
    {
        if (hand[i] != hand[0])
        {
            flush = false;
            break;
        }
    }

    if (flush)
    {
        return 10;
    }
    else if (quads > 0)
    {
        return 9;
    }
    else if (triples > 0 && pairs > 0)
    {
        return 8;
    }
    else if (triples > 0)
    {
        return 7;
    }
    else if (pairs == 2)
    {
        return 6;
    }
    else if (pairs == 1)
    {
        return 5;
    }
    else
        return 4;
}
int HandRank2(string hand)
{
    var max = 0;
    foreach (var genHand in ReplaceJokers(hand))
    {
        var handRank = HandRank(genHand);
        if (handRank > max)
            max = handRank;
    }
    return max;
}
int Rank(char c)
{
    if (c == 'T')
        return 10;
    if (c == 'J')
        return 11;
    if (c == 'Q')
        return 12;
    if (c == 'K')
        return 13;
    if (c == 'A')
        return 14;
    return c - '0';
}
int Rank2(char c)
{
    if (c == 'T')
        return 11;
    if (c == 'J')
        return 0;
    if (c == 'Q')
        return 12;
    if (c == 'K')
        return 13;
    if (c == 'A')
        return 14;
    return c - '0' + 1;
}

public class Hand
{
    public string Card;
    public int Bid;
    public int Pos;
}
