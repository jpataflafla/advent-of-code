string[] GetLinesOfInput(string filePath, bool showLinesCount = true)
{
    string[] lines = File.ReadAllLines(filePath);
    if(showLinesCount)
        Console.WriteLine($"{lines.Length} lines of input");
    return lines;
}

void RollRocksNorth(char[] column) // north - towards line 0
{
    var lastEmptyPosition = -1;
    var index = 0;
    while(index < column.Length)
    {
        if(column[index] == 'O' && lastEmptyPosition!=-1)  // rock
        {
            (column[index], column[lastEmptyPosition]) =
                (column[lastEmptyPosition], column[index]);
            
            index = lastEmptyPosition;
            lastEmptyPosition = -1;
            continue;
        }
        else if(column[index] == '#') // square rock/ obstacle
        {
            lastEmptyPosition = -1;
        }
        else if(column[index] == '.' && lastEmptyPosition == -1)// empty space
        {
            lastEmptyPosition = index;
        }
        index++;
    }
}

int ColumnPoints(char[] column)
{
    var sum = 0;
    for(var i = 0; i < column.Length; i++)
    {
        if(column[i] == 'O')
        {
            sum += (column.Length-i);
        }
    }
    return sum;
}


// Get input from provided file
var lines = GetLinesOfInput("input.txt");

var points = 0;
for(var i = 0; i < lines[0].Length; i++)
{
    var column = new char[lines[i].Length];

    for(var j = 0; j < lines.Length; j++)
    {
        column[j] = lines[j][i];    
    }
    Console.WriteLine(column);
    RollRocksNorth(column);
    Console.WriteLine(column);
    var columnPoints = ColumnPoints(column);
    points+=columnPoints;
    Console.WriteLine("\n");
}
Console.WriteLine(points);