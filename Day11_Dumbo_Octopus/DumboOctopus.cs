using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day11_Dumbo_Octopus
{
  class DumboOctopus
  {
    private IEnumerable<string> Input { get; } = File.ReadAllLines(@"D:\AoC\AoC2021Input\11\input.txt");

    private Dictionary<(int x, int y), Octopus> Map { get; } =
      new Dictionary<(int x, int y), Octopus>();

    private int Heigth { get; set; }

    private int Width { get; set; }

    private int NumberOfFlashes { get; set; }

    public int CurrentStep { get; set; }

    public DumboOctopus()
    {
      InitializeMap();
    }

    public void InitializeMap()
    {
      Map.Clear();
      Heigth = Input.Count();
      Width = Input.First().Length;

      for (int x = 0; x < Heigth; x++)
      for (int y = 0; y < Width; y++)
      {
        Map.Add((x, y), new Octopus(Input.ElementAt(x)[y] - 48));
      }
    }

    public int GetSolution1()
    {
      for (int step = 0; step < 100; step++)
      {
        InitializeNextStep();
        IgniteOctopi();
      }
      return NumberOfFlashes;
    }

    public int GetSolution2()
    {
      InitializeMap();
      Thread.Sleep(5000);
      while (true)
      {
        NumberOfFlashes = 0;
        CurrentStep++;
        InitializeNextStep();
        IgniteOctopi();

        Visualize();

        Thread.Sleep(500);

        if (NumberOfFlashes == Map.Count) return CurrentStep;
      }
    }

    private void IgniteOctopi()
    {
      bool done = false;
      while (!done)
      {
        done = true;
        for (int x = 0; x < Heigth; x++)
        {
          for (int y = 0; y < Width; y++)
          {
            if (Map[(x, y)].Power > 9)
            {
              Flash(x, y);
              done = false;
              break;
            }
          }

          if (done == false) break;
        }
      }
    }

    private void InitializeNextStep()
    {
      foreach (var octopus in Map.Values)
      {
        octopus.Power++;
        octopus.Flashed = false;
      }
    }

    private void Flash(int centerX, int centerY)
    {
      NumberOfFlashes++;
      var flashingOctopus = Map[(centerX, centerY)];
      flashingOctopus.Power = 0;
      flashingOctopus.Flashed = true;

      for (int x = centerX - 1; x <= centerX + 1; x++)
      {
        for (int y = centerY - 1; y <= centerY + 1; y++)
        {
          if (!(x < 0 || x >= Heigth || y < 0 || y >= Width))
          {
            var octopus = Map[(x, y)];
            if (!octopus.Flashed) octopus.Power++;
          }
        }
      }
    }

    private void Visualize()
    {
      for (int x = 0; x < Heigth; x++)
      {
        for (int y = 0; y < Width; y++)
        {
          var octopus = Map[(x, y)];
          if (octopus.Flashed)
          {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("O");
            Console.ResetColor();
          }
          else
          {
            Console.Write(octopus.Power);
          }
        }
        Console.Write("\n");
      }
      Console.Write("\n\n\n\n\n\n\n\n\n\n");
    }
  }
}
