using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02_Dive
{
  class Dive
  {
    public static string fileLocation = @"D:\AoC\2\input.txt";

    public static void Main1()
    {
      var commands = ReadFromFile();

      Console.WriteLine("Horizontal Position: " + commands['F']);
      Console.WriteLine("Depth: {0}", commands['D'] - commands['U']);
      Console.WriteLine("Solution: {0}", commands['F'] * (commands['D'] - commands['U']));
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
        
        while ((s = sr.ReadLine()) != null)
        {
          char key;
          int start;
          if (s.Contains("forward"))
          {
            key = 'F';
            start = 8;
            updateCommands(key, s, start, commands);
          }
          else if (s.Contains("down"))
          {
            key = 'D';
            start = 5;
            updateCommands(key, s, start, commands);
          }
          else if (s.Contains("up"))
          {
            key = 'U';
            start = 3;
            updateCommands(key, s, start, commands);
          }
          else
          {
            throw new IOException("Invalid line in input.txt");
          }
        }

        return commands;
      }
    }

    private static void updateCommands(char key, string line, int index, Dictionary<char, int> commands)
    {
      if (!commands.ContainsKey(key))
      {
        commands.Add(key, int.Parse(line.Substring(index)));
      }
      else
      {
        commands[key] += int.Parse(line.Substring(index));
      };
    }
  }
}
