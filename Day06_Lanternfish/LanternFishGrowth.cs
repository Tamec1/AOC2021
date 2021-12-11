using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06_Lanternfish
{
  class LanternFishGrowth
  {
    private IEnumerable<string> Input { get; } = File.ReadAllLines(@"D:\AoC\6\input.txt");
    public long[] Fish { get; } = new long[9];

    public LanternFishGrowth()
    {
      Input
        .First()
        .Split(',')
        .Select(int.Parse)
        .ToList()
        .ForEach(num => Fish[num]++);
    }

    public void GrowPopulationFor(int days)
    {
      // all fish have to be moved to the left each day
      for (int d = 0; d < days; d++)
      {
        long givingBirthFishes = Fish[0];
        Fish[0] = 0;
        for (int i = 0; i < 8; i++)
        {
          Fish[i] = Fish[i + 1];
          Fish[i + 1] = 0;
        }
        // all fish at day 0 have to be added to the fish at day 6
        Fish[6] += givingBirthFishes;
        // for every fish moved to day 6 a fish has to be added to day 8
        Fish[8] = givingBirthFishes;
      }
    }

    public long GetFishCount()
    {
      long totalCount = 0;
      foreach (var count in Fish)
      {
        totalCount += count;
      }
      return totalCount;
    }
  }
}
