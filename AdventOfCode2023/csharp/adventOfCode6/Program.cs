string[] GetLinesOfInput(string filePath, bool showLinesCount = true)
{
    string[] lines = File.ReadAllLines(filePath);
    if(showLinesCount)
        Console.WriteLine($"{lines.Length} lines of input");
    return lines;
}

/// <summary>
/// Parses text in the format "Name: 1 2 3 4" into an array of integers.
/// </summary>
/// <param name="input">The input text to parse.</param>
/// <returns>An array of integers parsed from the input text.</returns>
int[] ParseToIntArray(string textLine)
{
    string[] parts = textLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
    // Assuming the input format is "Name: 1 2 3 4", skipping the first element ("Name:")
    int[] values = new int[parts.Length - 1];
    
    for (int i = 1; i < parts.Length; i++)
    {
        if (int.TryParse(parts[i], out int value))
        {
            values[i - 1] = value;
        }
        else
        {
            throw new FormatException($"Error parsing element {i}: {parts[i]}");
        }
    }

    return values;
}

int GetNumberOfWaysToWin(int raceTime, int recordDistance)
{
    var counterOfWins = 0;
    int carChargingTime = raceTime - 1;
    while(carChargingTime > 0 && carChargingTime < raceTime)
    {
        var currentDistance = (raceTime - carChargingTime) * carChargingTime;
        if(recordDistance < currentDistance)
        {
            ++counterOfWins;
        }
        --carChargingTime;
    }
    return counterOfWins;
}


var lines = GetLinesOfInput("input.txt");

var times = ParseToIntArray(lines[0]);
Console.WriteLine("Times: " + string.Join(' ', times));
var records = ParseToIntArray(lines[1]);
Console.WriteLine("Records: " + string.Join(' ', records));

var waysToWin = new int[times.Length];
var combinationsOfWaysToWin = 1;
for(int i = 0; i < waysToWin.Length; i++)
{
    waysToWin[i] = GetNumberOfWaysToWin(times[i], records[i]);
    combinationsOfWaysToWin *= waysToWin[i];
}
Console.WriteLine($"Combinations of ways to win: {combinationsOfWaysToWin}");