
var isTesting = false;
var inputFile = isTesting ? "test.txt" : "input.txt";

var lines = File.ReadAllLines(inputFile);
var times  = lines[0].Split(':')[1]
    .Split(' ')
    .Where(s => !string.IsNullOrEmpty(s))
    .Select(int.Parse)
    .ToList();

var records = lines[1].Split(':')[1]
    .Split(' ')
    .Where(s => !string.IsNullOrEmpty(s))
    .Select(int.Parse)
    .ToList();

var recordBreakingHoldTimes = new List<int>();
for (var raceIndex = 0; raceIndex < times.Count; ++raceIndex)
{
    var raceTime = times[raceIndex];
    var recordDistance = records[raceIndex];

    var recordBreakingCount = 0;
    for (var msHeld = 0; msHeld < raceTime; ++msHeld)
    {
        var speed = msHeld;
        var distance = speed * (raceTime - msHeld);

        Console.WriteLine($"[Race {raceIndex}] Holding button for {msHeld}ms: {distance}mm");
        if (distance > recordDistance)
            recordBreakingCount++;
    }
    
    recordBreakingHoldTimes.Add(recordBreakingCount);
}

Console.WriteLine("\n=== Part One ===");
Console.WriteLine($"Product of record breaking counts is: {recordBreakingHoldTimes.Aggregate((acc, result) => result *= acc)}");

var partTwoRecordStr = records.Aggregate("", (current, record) => current + record.ToString());
var partTwoTimeStr = times.Aggregate("", (current, time) => current + time.ToString());

var partTwoTime = ulong.Parse(partTwoTimeStr);
var partTwoRecord = ulong.Parse(partTwoRecordStr);

var partTwoCount = 0;
for (ulong msHeld = 0; msHeld < partTwoTime; ++msHeld)
{
    var speed = msHeld;
    var distance = speed * (partTwoTime - msHeld);

    if (distance > partTwoRecord)
        partTwoCount++;
}

Console.WriteLine("=== Part Two ===");
Console.WriteLine($"Number of record breaking values: {partTwoCount}");
