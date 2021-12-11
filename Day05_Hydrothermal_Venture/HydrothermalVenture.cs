using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day05_Hydrothermal_Venture
{
  internal class HydrothermalVenture
  { 
    private IEnumerable<string> Input { get; } = File.ReadAllLines(@"D:\AoC\5\input.txt");
    public List<((int X, int Y) P1, (int X, int Y) P2)> Pairs { get; }
    public List<((int X, int Y) P1, (int X, int Y) P2)> Orthogonals { get; set; }
    public List<((int X, int Y) P1, (int X, int Y) P2)> Diagonals { get; set; }
    private const int MatrixDimension = 1000;
    private readonly int[,] Matrix = new int[MatrixDimension, MatrixDimension];

    public HydrothermalVenture()
    {
      Pairs = Input
        .Select(line =>
        {
          string[] s = Regex.Split(line, @"(\s->\s)");
          string[] first = s[0].Split(',');
          string[] second = s[2].Split(',');
          return (P1: (X: int.Parse(first[0]), Y: int.Parse(first[1])),
            P2: (X: int.Parse(second[0]), Y: int.Parse(second[1])));
        })
        .ToList();

      FindOrthogonals();

      FindDiagonals();

      FillMatrix();
    }

    public void FindOrthogonals()
    {
      Orthogonals = Pairs
        .Where(pair => pair.P1.X == pair.P2.X || pair.P1.Y == pair.P2.Y )
        .ToList();
    }

    private void FindDiagonals()
    {
      Diagonals = Pairs
        .Where(pair => pair.P1.X != pair.P2.X && pair.P1.Y != pair.P2.Y)
        .ToList();
    }

    public void FillMatrix()
    {
      foreach (var tuple in Orthogonals)
      {
        if (tuple.P1.X == tuple.P2.X)
        {
          for (int i = Math.Min(tuple.P1.Y, tuple.P2.Y); i <= Math.Max(tuple.P1.Y, tuple.P2.Y); i++)
          {
            Matrix[tuple.P1.X, i]++;
          }
        }

        if (tuple.P1.Y == tuple.P2.Y)
        {
          for (int i = Math.Min(tuple.P1.X, tuple.P2.X); i <= Math.Max(tuple.P1.X, tuple.P2.X); i++)
          {
            Matrix[i, tuple.P1.Y]++;
          }
        }
      }

      foreach (var tuple in Diagonals)
      {
        int minX = Math.Min(tuple.P1.X, tuple.P2.X);
        int minY = Math.Min(tuple.P1.Y, tuple.P2.Y);
        int maxY = Math.Max(tuple.P1.Y, tuple.P2.Y);

        for (int i = 0; i <= Math.Abs(tuple.P1.X - tuple.P2.X); i++)
        {
          if ((tuple.P1.X - tuple.P2.X) == (tuple.P1.Y - tuple.P2.Y))
          {
            Matrix[minX + i, minY + i]++;
          }
          else
          {
            Matrix[minX + i, maxY - i]++;
          }
        }
      }
    }

    public int CountCrossPoints()
    {
      int counter = 0;

      for (int i = 0; i < MatrixDimension; i++)
      {
        for (int j = 0; j < MatrixDimension; j++)
        {
          if (Matrix[i, j] > 1)
          {
            counter++;
          }
        }
      }

      return counter;
    }
  }
}
