using System;

namespace Day06_Lanternfish
{
  class Program
  {
    public static void Main()
    {
      var task = new LanternFishGrowth();

      task.GrowPopulationFor(256);

      Console.WriteLine(task.GetFishCount());
    }
  }
}
