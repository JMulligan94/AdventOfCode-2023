
var isTesting = false;
var inputPath = isTesting ? "test.txt" : "input.txt";

var partOneMaxAmount = new Dictionary<string, int>()
{
    ["red"] = 12,
    ["green"] = 13,
    ["blue"] = 14
};

var impossibleIds = new List<int>();
var possibleIds = new List<int>();
foreach (var gameStr in File.ReadLines(inputPath))
{
    var gameId = int.Parse(gameStr.Substring(0, gameStr.IndexOf(':')).Split(' ')[1]);
    var cubeSets = gameStr.Substring(gameStr.IndexOf(':') + 1).Split(';');
    possibleIds.Add(gameId);
    var isPossible = true;
    
    foreach (var cubeSet in cubeSets)
    {
        var cubes = cubeSet.Split(',');
        foreach (var cubeStr in cubes.Select(s => s.Trim()))
        {
            var cubeType = cubeStr.Split(' ')[1];
            var cubeAmount = int.Parse(cubeStr.Split(' ')[0]);

            if (cubeAmount <= partOneMaxAmount[cubeType])
                continue;

            possibleIds.Remove(gameId);
            impossibleIds.Add(gameId);
            isPossible = false;
            break;
        }

        if (!isPossible)
            break;
    }
} 

Console.WriteLine("=== Part One ===");
Console.WriteLine($"Possible games are: {string.Join(", ", possibleIds)}");
Console.WriteLine($"Impossible games are: {string.Join(", ", impossibleIds)}");
Console.WriteLine($"Sum of possible IDs: {possibleIds.Sum()}");


var cubeSetPowers = new List<int>();
foreach (var gameStr in File.ReadLines(inputPath))
{
    var cubeSetMaximums = new Dictionary<string, int>()
    {
        ["red"] = 0,
        ["green"] = 0,
        ["blue"] = 0
    };
    
    var cubeSets = gameStr.Substring(gameStr.IndexOf(':') + 1).Split(';');
    foreach (var cubeSet in cubeSets)
    {
        var cubes = cubeSet.Split(',');
        foreach (var cubeStr in cubes.Select(s => s.Trim()))
        {
            var cubeType = cubeStr.Split(' ')[1];
            var cubeAmount = int.Parse(cubeStr.Split(' ')[0]);

            if (cubeAmount > cubeSetMaximums[cubeType])
                cubeSetMaximums[cubeType] = cubeAmount;
        }
    }
    
    cubeSetPowers.Add(cubeSetMaximums["red"] * cubeSetMaximums["green"] * cubeSetMaximums["blue"]);
} 

Console.WriteLine("=== Part Two ===");
Console.WriteLine($"Game products are: {string.Join(", ", cubeSetPowers)}");
Console.WriteLine($"Sum of cube powers: {cubeSetPowers.Sum()}");