using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11_Dumbo_Octopus
{
  class Program
  {
    public static void Main()
    {
      var task = new DumboOctopus();

      Console.Write("###############\nSolution Part 1: ");
      Console.WriteLine(task.GetSolution1());
      Console.Write("###############\nSolution Part 2: ");
      Console.WriteLine(task.GetSolution2());
      Console.WriteLine();
    }
  }
}
