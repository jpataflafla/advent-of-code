using System.Text.RegularExpressions;

namespace adventOfCode4;

public class Card
{
    private static readonly Regex CardNumberRegex = new Regex(@"\d+");

    private readonly HashSet<string> _winningNumbers;// for small set a simple list is also fine

    private readonly string[] _cardNumbers;

    public int CardNumber { get; private set; }
    
    public int CountOfMatchingNumbers { get; private set; }

    public int CardSum { get; }

    public Card(string cardTextLine)
    {
        var split = cardTextLine.Split(':', '|');

        // this is not needed if the card numbers in the input file do not matter
        this.CardNumber = SetCardNumberFromName(split[0]);

        _winningNumbers = new HashSet<string>(split[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        _cardNumbers = split[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        CardSum = CalculateCardPoints();
    }

    private int SetCardNumberFromName(string name)
    {
        // Use regular expression to match the number in the string
        // the card number could also be assigned as just the index of the input line - without regex
        Match matchNumber = CardNumberRegex.Match(name);

        if (matchNumber.Success)
        {
            return int.Parse(matchNumber.Value);
        }
        throw new Exception("No number found in the name of the card.");
    }

    private int CalculateCardPoints()
    {
        var cardSum = 0;
        this.CountOfMatchingNumbers = 0;
        foreach (var n in _cardNumbers)
        {
            if (!_winningNumbers.Contains(n)) continue;
            cardSum = cardSum == 0 ? 1 : cardSum * 2;
            this.CountOfMatchingNumbers++;
        }

        return cardSum;
    }
}

internal static class Program
{
    private static string[] GetLinesOfInput(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        Console.WriteLine($"{lines.Length} lines of input");
        return lines;
    }

    public static void Main(string[] args)
    {
        var lines = GetLinesOfInput("input.txt");
        
        var sum = 0;
        var cardList = new int[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var card = new Card(line);
            sum += card.CardSum;
            cardList[i] += 1;
            if (card.CountOfMatchingNumbers == 0) continue;

            for (var j = 0; j < card.CountOfMatchingNumbers; j++)
            {
                cardList[card.CardNumber - 1 + j + 1] += cardList[i];
            }
        }

        var totalScratchcards = cardList.Sum();
        Console.WriteLine($"Total Scratchcards: {totalScratchcards}");
        // Console.WriteLine(string.Join("  ", cardList));
        Console.WriteLine($"sum: {sum}");
    }
}