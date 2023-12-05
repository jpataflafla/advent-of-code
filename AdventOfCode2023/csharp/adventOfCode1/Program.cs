using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

public class Program
{
  public static int IsNumber(string s, int index, string[] dict)
  {
    if (s[index] - '0' >= 0 && s[index] - '0' <= 9)
    {
      return s[index] - '0';
    }

    for (int i = 0; i < dict.Length; i++)
    {
      if (dict[i].Length > s.Length - index) continue;

      int j;
      for (j = 0; j < dict[i].Length; j++)
      {
        if (dict[i][j] != s[index + j])
        {
          break;  // a mismatch is found
        }
      }

      if (j == dict[i].Length)
      {
        // Entire pattern matched, return the index
        return i;
      }
    }

    return -1;
  }

  public static string[] GetLinesOfInput(string filePath)
  {
    string[] lines = File.ReadAllLines(filePath);
    Console.WriteLine($"{lines.Length} lines of input");
    return lines;
  }

  public static void Main(string[] args)
  {
    var lines = GetLinesOfInput("input.txt");

    var digitNames = new string[10]
    {"zero","one","two","three","four","five","six","seven","eight","nine"};

    var sum = 0;
    foreach (var line in lines)
    {
      int first = -1;
      int last = -1;
      for (int i = 0; i < line.Length; i++)
      {
        if (first == -1)
        {
          first = IsNumber(line, i, digitNames);
        }
        if (last == -1)
        {
          last = IsNumber(line, line.Length - 1 - i, digitNames);
        }
        if (last != -1 && first != -1)
        {
          break;
        }
      }

      if (first == -1 || last == -1) continue;

      Console.WriteLine($"line {line} | first: {first} | last: {last}");

      sum += (first * 10 + last);
    }
    Console.WriteLine($"sum: {sum}");
  }
}

