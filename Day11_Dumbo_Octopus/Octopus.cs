using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11_Dumbo_Octopus
{
  class Octopus
  {
    public int Power { get; set; }

    public bool Flashed { get; set; } = false;

    public Octopus(int power)
    {
      Power = power;
    }
  }
}
