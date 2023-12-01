
internal class Program
{
    public static void Main(string[] args)
    {
        var isTesting = false;
        var inputPath1 = isTesting ? "testA.txt" : "input.txt";

        var calibrationValues1 = new List<int>();
        foreach (var line in File.ReadLines(inputPath1))
        {
            var calibrationValueStr = string.Empty;
    
            // Find first digit
            for (var i = 0; i < line.Length; ++i)
            {
                if (IsDigit_PartOne(line, i))
                {
                    calibrationValueStr += line[i];
                    break;
                }
            }
    
            // Find last digit
            for (var i = line.Length -1 ; i >= 0; --i)
            {
                if (IsDigit_PartOne(line, i))
                {
                    calibrationValueStr += line[i];
                    break;
                }
            }

            calibrationValues1.Add(int.Parse(calibrationValueStr));
        }

        Console.WriteLine($"Calibration values are: {string.Join(", ", calibrationValues1)}\n");

        var calibrationSum1 = calibrationValues1.Sum();
        Console.WriteLine("=== Part One ===");
        Console.WriteLine($"Sum is: {calibrationSum1}\n\n");


        var inputPath2 = isTesting ? "testB.txt" : "input.txt";
        var calibrationValues2 = new List<int>();
        foreach (var line in File.ReadLines(inputPath2))
        {
            var calibrationValueStr = string.Empty;
    
            // Find first digit
            for (var i = 0; i < line.Length; ++i)
            {
                var returnedDigit = IsDigit_PartTwo(line, i);
                if (returnedDigit != null)
                {
                    calibrationValueStr += returnedDigit;
                    break;
                }
            }
    
            // Find last digit
            for (var i = line.Length -1 ; i >= 0; --i)
            {
                var returnedDigit = IsDigit_PartTwo(line, i);
                if (returnedDigit != null)
                {
                    calibrationValueStr += returnedDigit;
                    break;
                }
            }

            calibrationValues2.Add(int.Parse(calibrationValueStr));
        }

        Console.WriteLine($"Calibration values are: {string.Join(", ", calibrationValues2)}\n");

        var calibrationSum2 = calibrationValues2.Sum();
        Console.WriteLine("=== Part Two ===");
        Console.WriteLine($"Sum is: {calibrationSum2}\n\n");

    }

    private static bool IsDigit_PartOne(string line, int index)
    {
        return char.IsNumber(line[index]);
    }

    private static readonly List<string> s_typedDigits = new List<string>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
    private static string? IsDigit_PartTwo(string line, int index)
    {
        if (char.IsNumber(line[index]))
            return line[index].ToString();

        var matchingTypedIndex = s_typedDigits.FindIndex(d => line.Substring(index).StartsWith(d));
        if (matchingTypedIndex != -1)
            return (matchingTypedIndex + 1).ToString();
        
        return null;
    }
}