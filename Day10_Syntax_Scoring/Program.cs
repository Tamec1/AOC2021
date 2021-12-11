using System;

namespace Day10_Syntax_Scoring
{
  class Program
  {
    public static void Main()
    {
      var task = new SyntaxScoring();

      Console.Write("###############\nSolution Part 1: ");
      Console.WriteLine(task.GetSolution1());
      Console.Write("###############\nSolution Part 2: ");
      Console.WriteLine(task.GetSolution2());
      Console.WriteLine();
    }
  }
}
