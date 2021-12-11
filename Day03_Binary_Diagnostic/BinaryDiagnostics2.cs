using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03_Binary_Diagnostic
{
  class BinaryDiagnostics2
  {
    public static string FileLocation = @"D:\AoC\3\input.txt";

    public static void Main()
    {
      var input = ReadFromFile();
      var digits = input.First().Length;

      var oxygenRating = Convert.ToInt32(FindRating(digits, input, true), 2);
      var cO2SrubberRating = Convert.ToInt32(FindRating(digits, input, false), 2);

      Console.WriteLine($"Oxygen: {oxygenRating}");
      Console.WriteLine($"CO2 scrubber: {cO2SrubberRating}");

      Console.WriteLine($"\nLife Support Rating: {oxygenRating * cO2SrubberRating}");
    }

    private static List<string> ReadFromFile()
    {
      if (!File.Exists(FileLocation))
      {
        throw new FileNotFoundException("Could not load File at: " + FileLocation);
      }

      return new List<string>(File.ReadAllLines(FileLocation));
    }

    private static string FindRating(int digits, List<string> listToFilter, bool keepSetBits)
    {
      for (int digit = 0; digit < digits; digit++)
      {
        int zeroCounter = 0;
        int oneCounter = 0;

        foreach (var element in listToFilter)
        {
          if (element[digit] == '0')
          {
            zeroCounter++;
          }
          else
          {
            oneCounter++;
          }
        }

        char valueToKeep;
        if (keepSetBits)
        {
          valueToKeep = oneCounter >= zeroCounter ? '1' : '0';
        }
        else
        {
          valueToKeep = oneCounter < zeroCounter ? '1' : '0';
        }

        listToFilter = listToFilter.Where(element => element[digit] == valueToKeep).ToList();

        if (listToFilter.Count == 1)
        {
          return listToFilter.First();
        }
      }

      return listToFilter.First();
    }
  }
}