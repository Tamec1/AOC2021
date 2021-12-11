using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02_Dive
{
  class Dive2
  {
    public static string fileLocation = @"D:\AoC\2\input.txt";

    public static void Main()
    {
      var commands = ReadFromFile();

      Console.WriteLine("Horizontal Position: " + commands['F']);
      Console.WriteLine("Depth: {0}", commands['N']);
      Console.WriteLine("Solution: {0}", commands['F'] * commands['N']);
    }

    private static Dictionary<char, int> ReadFromFile()
    {
      if (!File.Exists(fileLocation))
      {
        throw new FileNotFoundException("Could not load File at: " + fileLocation);
      }

      List<int> values = new List<int>();
      using (StreamReader sr = File.OpenText(fileLocation))
      {
        var commands = new Dictionary<char, int>();

        string s;
        int aim = 0;
        int newDepth = 0;

        while ((s = sr.ReadLine()) != null)
        {
          char key;
          int start;
          if (s.Contains("forward"))
          {
            key = 'F';
            start = 8;
            newDepth += aim * UpdateCommands(key, s, start, commands);
          }
          else if (s.Contains("down"))
          {
            key = 'D';
            start = 5;
            aim += UpdateCommands(key, s, start, commands);
          }
          else if (s.Contains("up"))
          {
            key = 'U';
            start = 3;
            aim -= UpdateCommands(key, s, start, commands);
          }
          else
          {
            throw new IOException("Invalid line in input.txt");
          }
        }

        commands['A'] = aim;
        commands['N'] = newDepth;
        return commands;
      }
    }

    private static int UpdateCommands(char key, string line, int index, Dictionary<char, int> commands)
    {
      int value = int.Parse(line.Substring(index));

      if (!commands.ContainsKey(key))
      {
        commands.Add(key, value);
      }
      else
      {
        commands[key] += value;
      }

      return value;
    }
  }
}
