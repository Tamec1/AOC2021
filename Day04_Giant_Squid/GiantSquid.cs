using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day04_Giant_Squid
{
  class GiantSquid
  {
    public static string FileLocation = @"D:\AoC\4\input.txt";

    public static void Main()
    {
      var input = ReadFromFile(FileLocation);
      var numbers = input.Item1;
      var boards = input.Item2;
      Print(input);

      var round = 1;
      while (numbers.Any())
      {
        var calledNumber = CallNextNumber(numbers, boards);

        if (round > 4)
        {
          var winningBoard = CheckIfBingo(boards);
          if (winningBoard > -1)
          {
            Console.WriteLine($"Board Nr.{winningBoard} wins");

            // winning board found continue with point evaluation
            var finalScore = CalculateScore(boards[winningBoard], calledNumber);

            Console.WriteLine($"Final score: {finalScore}\n");
            break;
          }
        }
        round++;
      }
    }

    private static Tuple<Queue<int>, List<Cell[][]>> ReadFromFile(string fileLocation)
    {
      if (!File.Exists(FileLocation))
      {
        throw new FileNotFoundException("Could not load File at: " + FileLocation);
      }

      var numbers = new Queue<int>();
      List<Cell[][]> boards = new List<Cell[][]>();

      using (StreamReader sr = File.OpenText(FileLocation))
      {
        string firstLine = sr.ReadLine();

        if (firstLine != null)
        {
          firstLine.Split(',').Select(int.Parse).ToList().ForEach(num => numbers.Enqueue(num));
        }
        else
        {
          throw new FileLoadException("Could not read frist line from file");
        }

        while ((sr.ReadLine()) != null)
        {
          var board = new Cell[5][];

          for (int row = 0; row < 5; row++)
          {
            string s;
            if ((s = sr.ReadLine()) == null)
            {
              throw new FileLoadException("Could not read from file");
            }

            var values = Regex.Split(s.TrimStart(), @"\s+").Select(int.Parse).ToArray();

            var cells = new Cell[5];
            for (int field = 0; field < 5; field++)
            {
              cells[field] = new Cell(values[field]);
            }

            board[row] = cells;
          }

          boards.Add(board);
        }
      }

      return Tuple.Create(numbers, boards);
    }

    private static void Print(Tuple<Queue<int>, List<Cell[][]>> input)
    {
      var numbers = input.Item1;
      var boards = input.Item2;

      foreach (int number in numbers)
      {
        Console.WriteLine(number);
      }
      Console.Write("\n");

      foreach (var board in boards)
      {
        foreach (var row in board)
        {
          foreach (Cell i in row)
          {
            Console.Write($"{i.Number} ");
          }
          Console.Write("\n");
        }
        Console.Write("\n");
      }
    }

    private static int CallNextNumber(Queue<int> numbers, List<Cell[][]> boards)
    {
      var number = numbers.Dequeue();

      foreach (var board in boards)
      {
        // travers rows
        foreach (var row in board)
        {
          // travers fields
          foreach (var cell in row)
          {
            // check if number is found
            if (cell.Number == number)
            {
              // if number is found mark cell
              cell.IsMarked = true;
            }
          }
        }
      }

      return number;
    }

    private static int CheckIfBingo(List<Cell[][]> boards)
    {
      for (int boardIndex = 0; boardIndex < boards.Count; boardIndex++)
      {
        var board = boards[boardIndex];
        // check rows
        for (int rowIndex = 0; rowIndex < board.Length; rowIndex++)
        {
          var row = board[rowIndex];
          // check columns
          for (int cellIndex = 0; cellIndex < row.Length; cellIndex++)
          {
            if (!row[cellIndex].IsMarked)
            {
              break;
            }
            // if all cells marked -> Bingo Found!
            if (cellIndex == 4)
            {
              return boardIndex;
            }
          }
        }

        // check columns
        for (int cellIndex = 0; cellIndex < board.Length; cellIndex++)
        {
          // check rows
          for (int rowIndex = 0; rowIndex < board.Length; rowIndex++)
          {
            if (!board[rowIndex][cellIndex].IsMarked)
            {
              break;
            }
            // if all cells marked -> Bingo Found!
            if (rowIndex == 4)
            {
              return boardIndex;
            }
          }
        }

        // check diagonals

        // check falling diagonal
        for (int index = 0; index < board.Length; index++)
        {
          if (!board[index][index].IsMarked)
          {
            break;
          }
          // if all cells marked -> Bingo Found!
          if (index == 4)
          {
            return boardIndex;
          }
        }

        // check rising diagonal
        for (int index = board.Length-1; index >= 0; index--)
        {
          if (!board[index][index].IsMarked)
          {
            break;
          }
          // if all cells marked -> Bingo Found!
          if (index == 0)
          {
            return boardIndex;
          }
        }
      }

      return -1;
    }

    private static int CalculateScore(Cell[][] board, int calledNumber)
    {
      var currentScore = 0;

      foreach (var row in board)
      {
        foreach (var cell in row)
        {
          if (!cell.IsMarked)
          {
            currentScore += cell.Number;
          }
        }
      }

      return currentScore * calledNumber;
    }
  }
}
