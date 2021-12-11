using System;

namespace Day08_Seven_Segment_Search
{
  class Program
  {
    public static void Main()
    {
      var task = new SevenSegmentSearch();

      Console.WriteLine($"Number of segments for digits 1, 4, 7, 8: {task.GetSolution1()}");

      Console.WriteLine($"Sum of all Number Outputs: {task.GetSolution2()}");
    }
  }
}
