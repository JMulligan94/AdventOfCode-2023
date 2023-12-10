
var isTesting = true;
var inputFile = isTesting ? "test.txt" : "input.txt";

var seeds = new List<ulong>();
var seedRanges = new List<(ulong, ulong)>();

var maps = new List<List<Mapping>>();
var currentIndex = -1;
foreach (var inputLine in File.ReadLines(inputFile))
{
    if (inputLine == string.Empty)
        continue;
    
    if (inputLine.StartsWith("seeds:"))
    {
        seeds = inputLine.Substring("seeds: ".Length)
            .Split(' ')
            .Select(ulong.Parse)
            .ToList();

        for (var seedIndex = 0; seedIndex < seeds.Count; seedIndex+=2)
        {
            seedRanges.Add((seeds[seedIndex], seeds[seedIndex+1]));
        }
        continue;
    }

    if (char.IsLetter(inputLine[0]))
    {
        // Start of new map
        currentIndex++;
        maps.Add(new List<Mapping>());
        continue;
    }

    var mappingInfo = inputLine.Split(' ');
    maps.Last().Add(new Mapping()
    {
        Source = ulong.Parse(mappingInfo[1]),
        Dest = ulong.Parse(mappingInfo[0]),
        Size = ulong.Parse(mappingInfo[2]),
    });
}

var partOneLocations = new List<ulong>();
foreach (var seed in seeds)
{
    var seedValue = seed;
    foreach(var mappingSet in maps)
    {
        var matchingMapping = mappingSet
            .FirstOrDefault(mapping => seedValue >= mapping.SourceRange.Item1
                                       && seedValue <= mapping.SourceRange.Item2);

        if (matchingMapping != null)
            seedValue += matchingMapping.Offset;
    }
    
    Console.WriteLine($"Seed: {seed}, Location {seedValue}");
    partOneLocations.Add(seedValue);
}

Console.WriteLine("\n=== Part One ===");
Console.WriteLine($"Lowest location number is: {partOneLocations.Min()}");

class Mapping
{
    public ulong Source { get; init; }
    public ulong Dest { get; init; }
    public ulong Size { get; init; }

    public ulong Offset => Dest - Source;

    public (ulong,ulong) SourceRange => (Source, Source + Size - 1);
    public (ulong,ulong) DestRange => (Dest, Dest + Size - 1);
} 