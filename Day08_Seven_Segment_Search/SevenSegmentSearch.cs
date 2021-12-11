using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day08_Seven_Segment_Search
{
  class SevenSegmentSearch
  {
    private IEnumerable<string> Input { get; } = File.ReadAllLines(@"D:\AoC\8\input.txt");

    private List<List<string>> Patterns { get; } = new List<List<string>>();

    private List<List<string>> OutputValues { get; } = new List<List<string>>();

    public SevenSegmentSearch()
    {
      Input
        .ToList()
        .ForEach(line =>
          {
            string[] fragment = Regex.Split(line, @"(\s[|]\s)");
            Patterns.Add(fragment[0].Split().Select(s => String.Concat(s.OrderBy(c => c))).ToList());
            OutputValues.Add(fragment[2].Split().Select(s => String.Concat(s.OrderBy(c => c))).ToList());
          });
    }

    public int GetSolution1()
    {
      var validLengths = new List<int> {2, 3, 4, 7};

      return OutputValues.SelectMany(l => l).Count(s => validLengths.Contains(s.Length));
    }

    public int GetSolution2()
    {
      int finalSum = 0;

      for (int i = 0; i < Patterns.Count; i++)
      {
        var pattern = Patterns[i];
        var output = OutputValues[i];

        var keys = new string[10];

        keys[1] = pattern.First(s => s.Length == 2);
        pattern.Remove(keys[1]);

        keys[4] = pattern.First(s => s.Length == 4);
        pattern.Remove(keys[4]);

        keys[7] = pattern.First(s => s.Length == 3);
        pattern.Remove(keys[7]);

        keys[8] = pattern.First(s => s.Length == 7);
        pattern.Remove(keys[8]);

        keys[9] = pattern.First(s => s.Length == 6 && keys[4].All(s.Contains));
        pattern.Remove(keys[9]);

        keys[5] = pattern
          .First(s => s.Length == 5 && keys[4]
            .Replace(keys[1][0].ToString(), string.Empty)
            .Replace(keys[1][1].ToString(), string.Empty)
            .All(s.Contains));
        pattern.Remove(keys[5]);

        keys[6] = pattern.First(s => s.Length == 6 && keys[5].All(s.Contains));
        pattern.Remove(keys[6]);

        keys[0] = pattern.First(s => s.Length == 6);
        pattern.Remove(keys[0]);

        keys[3] = pattern.First(s => s.Length == 5 && keys[1].All(s.Contains));
        pattern.Remove(keys[3]);

        keys[2] = pattern.First(s => s.Length == 5);
        pattern.Remove(keys[2]);

        var translator = new Dictionary<string, int>();

        for (int val = 0; val < 10; val++)
        {
          translator[keys[val]] = val;
        }

        int power = 1000;
        foreach (var digit in output)
        {
          finalSum += translator[digit] * power;
          power = power > 10 ? (power / 10) : 1;
        }
      }
      return finalSum;
    }
  }
}
