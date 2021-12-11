using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05_Hydrothermal_Venture
{
  class Program
  {
    public static void Main()
    {
      var task = new HydrothermalVenture();

      Console.WriteLine($"Counted Crosspoints: {task.CountCrossPoints()}");
    }
  }
}
