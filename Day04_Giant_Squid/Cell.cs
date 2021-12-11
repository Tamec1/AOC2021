using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04_Giant_Squid
{
  class Cell
  {
    public int Number { get; set; }
    public bool IsMarked { get; set; } = false;

    public Cell(int number)
    {
      Number = number;
    }
  }
}
