
var isTesting = false;
var inputFile = isTesting ? "test.txt" : "input.txt";

var cardScores = new List<int>();

foreach (var line in File.ReadLines(inputFile))
{
    var playerNumbersStr = line.Substring(line.IndexOf('|') + 1);
    var winningNumberStr = line.Substring(0, line.IndexOf('|'));
    
    var winningNumbers = winningNumberStr.Split(' ')
        .Where(num => num != string.Empty)
        .Skip(2)
        .Select(int.Parse)
        .ToList();
    
    var playerNumbers = playerNumbersStr.Split(' ')
        .Where(num => num != string.Empty)
        .Select(int.Parse)
        .ToList();
    
    cardScores.Add(playerNumbers.Count(playerNum => winningNumbers.Contains(playerNum)));
}

Console.WriteLine("=== Part One ===");
Console.WriteLine($"Card score sum: {cardScores.Where(score=> score > 0).Select(score => 1 << (score-1)).Sum()}");

var cardAmounts = Enumerable.Range(0,cardScores.Count).Select(n => 1).ToArray();
for (var i = 0; i < cardScores.Count; ++i)
{
    var score = cardScores[i];
    for (var j = i+1; j <= i+score; ++j)
    {
        cardAmounts[j] += cardAmounts[i];
    }
}

Console.WriteLine("\n=== Part Two ===");
Console.WriteLine($"Amount of scratchcards in total: {cardAmounts.Sum()}");