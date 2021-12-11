using System;

namespace Day07_The_Treachery_Of_Whales
{
  class Program
  {
    public static void Main()
    {
      var task = new Whales();

      Console.WriteLine($"Minimal fuel consumption for linear fuel costs = {task.GetSolution1()}");

      Console.WriteLine($"Minimal fuel consumption for rising fuel costs = {task.GetSolution2()}");
    }
  }
}
