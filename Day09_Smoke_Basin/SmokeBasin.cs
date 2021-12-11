using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day09_Smoke_Basin
{
  class SmokeBasin
  {
    private const string FileLocation = @"D:\AoC\AoC2021Input\9";

    private const int MapDimension = 100;

    private const int MaxIndex = 99;

    private int[,] Map { get; } = new int[MapDimension, MapDimension];

    Dictionary<(int x, int y), int> Minima = new Dictionary<(int x, int y), int>();

    HashSet<(int x, int y)> Tiles = new HashSet<(int x, int y)>();

    Queue<(int x, int y)> Tasks = new Queue<(int x, int y)>();

    public SmokeBasin()
    {
      ReadInput();
      FindMinima();

    }

    private void ReadInput()
    {
      using (StreamReader sr = File.OpenText(FileLocation))
      {
        int row = 0;
        string s;
        while ((s = sr.ReadLine()) != null)
        {
          char[] chars = s.ToCharArray(0, MapDimension);

          for (int i = 0; i < MapDimension; i++)
          {
            Map[row, i] = chars[i] - 48;
          }

          row++;
        }
      }
    }

    public int FindMinima()
    {
      for (int y = 0; y <= MaxIndex; y++)
      for (int x = 0; x <= MaxIndex; x++)
      {
        var num = Map[y, x];
        if ((y == 0 || num < Map[y - 1, x])
            && (y == MaxIndex || num < Map[y + 1, x])
            && (x == 0 || num < Map[y, x - 1])
            && (x == MaxIndex || num < Map[y, x + 1]))
        {
          Minima[(y, x)] = Map[y, x];
        }
      }

      return Minima.Values.Sum(x => x + 1);
    }

    public int GetSolution2()
    {
      var sizes = new List<int>();

      foreach (var min in Minima)
      {
        sizes.Add(GetBasinSize(min.Key));
      }

      return sizes.OrderByDescending(x => x).Take(3).Aggregate((a, x) => a * x);
    }

    private int GetBasinSize((int x, int y) min)
    {
      Tiles.Clear();
      Tasks.Enqueue(min);

      while (Tasks.Any())
      {
        GetTiles(Tasks.Dequeue());
      }

      return Tiles.Count(tile => Map[tile.x, tile.y] != 9);
    }

    private void GetTiles((int x, int y) pos)
    {
      if (!Tiles.Contains(pos))
      {
        Tiles.Add(pos);
        var num = Map[pos.x, pos.y];

        if (num == 9) return;

        // Tile to the right
        AddWork(num, 0, -1);
        // Tile to the left
        AddWork(num, 0, 1);
        // Tile above
        AddWork(num, -1, 0);
        // Tile below
        AddWork(num, 1, 0);
      }

      void AddWork(int num, int dx, int dy)
      {
        var nx = pos.x + dx;
        var ny = pos.y + dy;
        var npos = (nx, ny);

        if (!Tiles.Contains(npos)
            && nx >= 0 && nx <= MaxIndex
            && ny >= 0 && ny <= MaxIndex
            && num < Map[nx, ny])
        {
          Tasks.Enqueue(npos);
        }
      }
    }
  }
}
