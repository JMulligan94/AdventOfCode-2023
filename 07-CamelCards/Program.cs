

var isTesting = true;
var inputFile = isTesting ? "test.txt" : "input.txt";
var partOneHands = new List<Hand>();
var partTwoHands = new List<Hand>();
foreach (var line in File.ReadLines(inputFile))
{
    partOneHands.Add(new Hand(line.Split(' ')[0], int.Parse(line.Split(' ')[1]), 1));
    partTwoHands.Add(new Hand(line.Split(' ')[0], int.Parse(line.Split(' ')[1]), 2));
}

var handIndex = partOneHands.Count();
var groupedHands = partOneHands.GroupBy(h => h.HandType).OrderBy(group => group.Key).ToList();
var totalWinnings = 0;
foreach (var handTypeGroup in groupedHands)
{
    var handTypeList = handTypeGroup.ToList();
    handTypeList.Sort();
    handTypeList.Reverse();
    foreach (var hand in handTypeList)
    {
        var handRank = handIndex;
        Console.WriteLine($"Rank {handRank}:\t{hand.Cards}\t{hand.Bet}\t{hand.HandType}");
        totalWinnings += handRank * hand.Bet;
        handIndex--;
    }
}

Console.WriteLine("\n=== Part One ===");
Console.WriteLine($"Total winnings from all hands: {totalWinnings}");

class Hand : IComparable
{
    public string Cards { get; init; }
    public int Bet { get; init; }
    public HandType HandType { get; set; }
    
    private static List<char> s_cardTypesPartOne = new() { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
    private static List<char> s_cardTypesPartTwo = new() { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

    private int m_partNumber = 1;

    public Hand(string cards, int bet, int partNumber)
    {
        Cards = cards;
        Bet = bet;
        m_partNumber = partNumber;

        if (m_partNumber == 1)
        {
            ScoreHandPartOne();
        }
        else
        {
            ScoreHandPartTwo();
        }
    }

    private void ScoreHandPartOne()
    {
        var groupedCards = Cards.GroupBy(c => c).OrderByDescending(group => group.Count()).ToList();
        if (groupedCards[0].Count() == 5)
        {
            HandType = HandType.FiveOfAKind;
        }
        else if (groupedCards[0].Count() == 4)
        {
            HandType = HandType.FourOfAKind;
        }
        else if (groupedCards[0].Count() == 3 && groupedCards[1].Count() == 2)
        {
            HandType = HandType.FullHouse;
        }
        else if (groupedCards[0].Count() == 3)
        {
            HandType = HandType.ThreeOfAKind;
        }
        else if (groupedCards[0].Count() == 2 && groupedCards[1].Count() == 2)
        {
            HandType = HandType.TwoPair;
        }
        else if (groupedCards[0].Count() == 2)
        {
            HandType = HandType.OnePair;
        }
        else
        {
            HandType = HandType.HighCard;
        }
    }
    
    private void ScoreHandPartTwo()
    {
        var jokerCount = Cards.Count(c => c == 'J');
        var groupedCards = Cards.GroupBy(c => c).OrderByDescending(group => group.Count()).ToList();
        if (groupedCards[0].Count() == 5)
        {
            HandType = HandType.FiveOfAKind;
        }
        else if (groupedCards[0].Count() == 4)
        {
            HandType = HandType.FourOfAKind;
            if (groupedCards[0].Key != 'J' && jokerCount == 1)
                HandType = HandType.FiveOfAKind;
        }
        else if (groupedCards[0].Count() == 3 && groupedCards[1].Count() == 2)
        {
            HandType = HandType.FullHouse;
            if (groupedCards[0].Key != 'J' && jokerCount == 1)
                HandType = HandType.FourOfAKind;
            else if (jokerCount == 2)
                HandType = HandType.FiveOfAKind;
        }
        else if (groupedCards[0].Count() == 3)
        {
            HandType = HandType.ThreeOfAKind;
            if (jokerCount == 1)
                HandType = HandType.FourOfAKind;
            else if (jokerCount == 2)
                HandType = HandType.FiveOfAKind;
        }
        else if (groupedCards[0].Count() == 2 && groupedCards[1].Count() == 2)
        {
            HandType = HandType.TwoPair;
        }
        else if (groupedCards[0].Count() == 2)
        {
            HandType = HandType.OnePair;
        }
        else
        {
            HandType = HandType.HighCard;
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Hand otherHand)
            return 0;

        var cardRankings = m_partNumber == 1 ? s_cardTypesPartOne : s_cardTypesPartTwo;
        var cardCheckIndex = 0;
        while (cardCheckIndex < Cards.Length)
        {
            var cardCompareResult = cardRankings.IndexOf(Cards[cardCheckIndex]).CompareTo(cardRankings.IndexOf(otherHand.Cards[cardCheckIndex]));
            if (cardCompareResult != 0)
                return cardCompareResult;
            cardCheckIndex++;
        }
        
        return 0;
    }
}

enum HandType
{
    FiveOfAKind, 
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}

