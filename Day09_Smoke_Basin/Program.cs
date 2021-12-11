using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09_Smoke_Basin
{
  class Program
  {
    public static void Main()
    {
      var task = new SmokeBasin();

      Console.WriteLine(task.FindMinima());

      Console.WriteLine(task.GetSolution2());
    }
  }
}
