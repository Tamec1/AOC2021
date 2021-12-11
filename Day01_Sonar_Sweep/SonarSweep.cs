using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01_Sonar_Sweep
{
  class SonarSweep
  {
    public static string fileLocation = @"D:\AoC\1\input.txt";

    public static void Main()
    {
      var input = ReadFromFile();

      Console.WriteLine(CountIncreasedElements(input));

      Console.WriteLine(CountTripleSlidingWindow(input));
    }

    private static List<int> ReadFromFile()
    {
      if (!File.Exists(fileLocation))
      {
        throw new FileNotFoundException("Could not load File at: " + fileLocation);
      }

      List<int> values = new List<int>();
      using (StreamReader sr = File.OpenText(fileLocation))
      {
        string s;
        while ((s = sr.ReadLine()) != null)
        {
          values.Add(int.Parse(s));
        }
      }

      return values;
    }

    private static int CountIncreasedElements(List<int> input)
    {
      int counter = 0;

      for (int i = 1; i < input.Count; i++)
      {
        if (input[i] > input[i - 1])
        {
          counter++;
        }
      }

      return counter;
    }

    private static int CountTripleSlidingWindow(List<int> input)
    {
      var counter = 0;
      var previousSum = int.MaxValue;

      for (int i = 0; i < input.Count - 2; i++)
      {
        var sum = input[i] + input[i + 1] + input[i + 2];
        if (sum > previousSum)
        {
          counter++;
        }

        previousSum = sum;
      }

      return counter;
    }
  }
}
