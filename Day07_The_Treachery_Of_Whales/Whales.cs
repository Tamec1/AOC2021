using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07_The_Treachery_Of_Whales
{
  class Whales
  {
    private IEnumerable<string> Input { get; } = File.ReadAllLines(@"D:\AoC\7\input.txt");

    public List<int> Positions { get; }

    public Whales()
    {
      Positions = Input
        .First()
        .Split(',')
        .Select(int.Parse)
        .ToList();
    }

    public int GetSolution1()
    {
      return CalculateMinimum(true);
    }

    public int GetSolution2()
    {
      return CalculateMinimum(false);
    }

    private int CalculateMinimum(bool linearFuelConsumption)
    {
      List<int> fuelConsumptions = new List<int>();

      for (int target = Positions.AsQueryable().Min(); target < Positions.AsQueryable().Max(); target++)
      {
        fuelConsumptions.Add(linearFuelConsumption
          ? CalculateLinearFuelConsumption(target)
          : CalculateRisingFuelConsumption(target));
      }

      return fuelConsumptions.AsQueryable().Min();
    }

    private int CalculateLinearFuelConsumption(int target)
    {
      int fuel = 0;
      foreach (var initialPosition in Positions)
      {
        fuel += Math.Abs(target - initialPosition);
      }

      return fuel;
    }

    private int CalculateRisingFuelConsumption(int target)
    {
      int fuel = 0;
      foreach (var initialPosition in Positions)
      {
        var distance = Math.Abs(target - initialPosition);
        for (int position = distance; position > 0; position--)
        {
          fuel += position;
        }
      }

      return fuel;
    }
  }
}
