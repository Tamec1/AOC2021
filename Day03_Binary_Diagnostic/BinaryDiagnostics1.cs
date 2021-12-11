using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03_Binary_Diagnostic
{
  class BinaryDiagnostics1
  {
    public static string FileLocation = @"D:\AoC\3\input.txt";

    public static void Main1()
    {
      var input = ReadFromFile();
      var lineLen = input.First().Length;
      var sb = new StringBuilder();

      var counts = new int[lineLen];

      foreach (var line in input)
      {
        for (int i = 0; i < lineLen; i++)
        {
          if (line[i] == '1')
          {
            counts[i]++;
          }
        }
      }

      foreach (var count in counts)
      {
        Console.WriteLine(count);
        sb.Append(count > (input.Count / 2) ? '1' : '0');
      }

      Console.WriteLine(sb.ToString());

      var gammaString = sb.ToString();

      ushort gamma = Convert.ToUInt16(gammaString, 2);
      ushort epsilon = (ushort)(61440 ^ ~gamma);

      Console.WriteLine($"Gamma: {gamma}");
      Console.WriteLine($"Epsilon: {epsilon}");

      Console.WriteLine($"\nPower Consumption: {gamma * epsilon}");
    }

    private static List<string> ReadFromFile()
    {
      if (!File.Exists(FileLocation))
      {
        throw new FileNotFoundException("Could not load File at: " + FileLocation);
      }

      List<string> values = new List<string>(File.ReadAllLines(FileLocation));

      return values;
    }
  }
}
