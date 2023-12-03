
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

internal class Program
{
    public static string GetGearId(int row, int col) => $"{col},{row}";
    public static void Main(string[] args)
    {
        var isTesting = false;
        var inputFile = isTesting ? "test.txt" : "input.txt";

        var lines = File.ReadAllLines(inputFile).ToList();

        var partNumbers = new List<int>();
        var failedNumbers = new List<int>();
        var gears = new Dictionary<string, List<int>>();
        for (var row = 0; row < lines.Count; ++row)
        {
            var rowLine = lines[row];
            for (var col = 0; col < rowLine.Length; ++col)
            {
                if (!char.IsDigit(rowLine[col]))
                    continue;

                //Console.WriteLine($"Found start of possible part number at ({col},{row}): '{rowLine[col]}'");
                var possiblePartString = rowLine[col].ToString();
                var includedColumns = new List<int> { col };
                col++;
                while (col < rowLine.Length && char.IsDigit(rowLine[col]))
                {
                    includedColumns.Add(col);
                    possiblePartString += rowLine[col];
                    col++;
                }

                // Console.WriteLine($"Full number at ({col},{row}) is: '{possiblePartString}'");
                //Console.WriteLine($"Check row {row}, columns {string.Join(", ", includedColumns)} for neighbouring symbols");

                // Is a part number if at least one digit has a symbol adjacent (including diagonally)
                var isPartNumber = false;

                foreach (var includedCol in includedColumns)
                {
                    // North
                    if (row > 0 && CheckCoords(lines, row-1, includedCol, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
                    
                    // North East
                    if (includedCol < rowLine.Length-1 && row > 0 && CheckCoords(lines, row-1, includedCol+1, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
                    // East
                    if (includedCol < rowLine.Length-1 && CheckCoords(lines, row, includedCol+1, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
                    // South East
                    if (includedCol < rowLine.Length-1 && row < lines.Count-1 && CheckCoords(lines, row+1, includedCol+1, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
                    // South
                    if (row < lines.Count-1 && CheckCoords(lines, row+1, includedCol, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
                    // South West
                    if (includedCol > 0 && row < lines.Count-1 && CheckCoords(lines, row+1, includedCol-1, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
                    // West
                    if (includedCol > 0 && CheckCoords(lines, row, includedCol-1, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
                    // North West
                    if (includedCol > 0 && row > 0 && CheckCoords(lines, row-1, includedCol-1, possiblePartString, ref gears))
                    {
                        isPartNumber = true;
                        break;
                    }
            
                }
        
                if (isPartNumber)
                    partNumbers.Add(int.Parse(possiblePartString));
                else
                    failedNumbers.Add(int.Parse(possiblePartString));
            }
        }


        Console.WriteLine("\n=== Part One ===");
        Console.WriteLine($"List of part numbers: {string.Join(", ", partNumbers)}");
        Console.WriteLine($"List of failed numbers: {string.Join(", ", failedNumbers)}");
        Console.WriteLine($"\nSum of part numbers: {partNumbers.Sum()}");

        Console.WriteLine("\n=== Part Two ===");
        var validGears = gears.Where(g => g.Value.Count == 2).ToList();
        Console.WriteLine($"List of gears with *exactly two* part numbers: {string.Join(", ", validGears.Select(g => $"[({g.Key}):{string.Join('*', g.Value)}]"))}");

        var gearRatios = validGears.Select(g => g.Value[0] * g.Value[1]).ToList();
        Console.WriteLine($"List of gear ratios: {string.Join(", ", gearRatios)}");
        Console.WriteLine($"\nSum of gear ratios: {gearRatios.Sum()}");

    }

    static bool CheckCoords(List<string> lines, int row, int col, string partString, ref Dictionary<string, List<int>> gears)
    {
        if (!IsSymbol(lines[row][col]))
            return false;
        
        if (IsGear(lines[row][col]))
        {
            var gearId = GetGearId(row, col);
            if (!gears.ContainsKey(gearId))
                gears.Add(gearId, new());
            gears[gearId].Add(int.Parse(partString));
        }

        return true;
    }
    
    static bool IsSymbol(char testChar) => testChar != '.' && !char.IsDigit(testChar);
    static bool IsGear(char testChar) => testChar == '*';
}
