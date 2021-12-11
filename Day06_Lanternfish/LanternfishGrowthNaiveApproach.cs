using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06_Lanternfish
{
  class LanternfishGrowthNaiveApproach
  {
    private IEnumerable<string> Input { get; } = File.ReadAllLines(@"D:\AoC\6\input.txt");

    internal class LanternFish
    {
      public int DaysUntilReproduction { get; set; }

      public LanternFish(int daysUntilReproduction)
      {
        DaysUntilReproduction = daysUntilReproduction;
      }

      public void AgeOneDay()
      {
        if (--DaysUntilReproduction < 0)
        {
          NewBorn.Add(new LanternFish(8));
          DaysUntilReproduction = 6;
        }
      }
    }

    public static List<LanternFish> Population { get; set; }

    private static List<LanternFish> NewBorn { get; set; }

    public LanternfishGrowthNaiveApproach()
    {
      Population = Input
        .First()
        .Split(',')
        .Select(int.Parse)
        .Select(num => new LanternFish(num))
        .ToList();

      NewBorn = new List<LanternFish>();
    }

    public void NextDay()
    {
      foreach (var lanternFish in Population)
      {
        lanternFish.AgeOneDay();
      }

      Population.AddRange(NewBorn);
    }

    public void PrintPopulation()
    {
      foreach (var lanternFish in Population)
      {
        Console.Write($"{lanternFish.DaysUntilReproduction},");
      }
    }

    public int GetPopulationSize()
    {
      return Population.Count;
    }
  }
}
