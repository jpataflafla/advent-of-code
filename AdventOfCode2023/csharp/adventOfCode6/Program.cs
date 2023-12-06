string[] GetLinesOfInput(string filePath, bool showLinesCount = true)
{
    string[] lines = File.ReadAllLines(filePath);
    if(showLinesCount)
        Console.WriteLine($"{lines.Length} lines of input");
    return lines;
}

/// <summary>
/// Parses text in the format "Name: 1 2 3 4" into an array of numbers.
/// </summary>
/// <param name="input">The input text to parse.</param>
/// <returns>An array of numbers parsed from the input text.</returns>
ulong[] ParseToNumberArray(string textLine)
{
    string[] parts = textLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
    // Assuming the input format is "Name: 1 2 3 4", skipping the first element ("Name:")
    ulong[] values = new ulong[parts.Length - 1];
    
    for (var i = 1; i < parts.Length; i++)
    {
        if (ulong.TryParse(parts[i], out ulong value))
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

ulong GetNumberOfWaysToWin(ulong raceTime, ulong recordDistance)
{
    ulong counterOfWins = 0;
    var carChargingTime = raceTime - 1;
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

ulong MakeNumberFromIntArray(ulong[] array)
{
    if (array == null || array.Length == 0)
    {
        throw new ArgumentException("Array cannot be null or empty.");
    }

    // Concatenate the digits to form the number
    string concatenatedDigits = string.Join("", array);

    if (!ulong.TryParse(concatenatedDigits, out ulong result))
    {
        throw new FormatException($"Error parsing concatenated digits: {concatenatedDigits}");
    }
    return result;
}

var lines = GetLinesOfInput("input.txt");

// Part 1
var times = ParseToNumberArray(lines[0]);
Console.WriteLine("Times: " + string.Join(' ', times));
var records = ParseToNumberArray(lines[1]);
Console.WriteLine("Records: " + string.Join(' ', records));

ulong combinationsOfWaysToWin = 1;
for(int i = 0; i < times.Length; i++)
{
    combinationsOfWaysToWin *= GetNumberOfWaysToWin(times[i], records[i]);
}
Console.WriteLine($"Combinations of ways to win: {combinationsOfWaysToWin}");

// Part 2
Console.WriteLine("Treat Times and Recors input as one Time int and one Record int (igonre whitespaces)");
var numberOfWaysToWinOneRace = GetNumberOfWaysToWin(
    MakeNumberFromIntArray(times),
    MakeNumberFromIntArray(records));

Console.WriteLine($"Combinations of ways to win one race (part 2 of the challange): {numberOfWaysToWinOneRace}");
