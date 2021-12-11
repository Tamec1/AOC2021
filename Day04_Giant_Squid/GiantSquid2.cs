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

  // CORRECT ANSWER SHOULD BE 12833 -- correct board is 24?

  class GiantSquid2
  {
    public static string FileLocation = @"D:\AoC\4\input.txt";

    const int BoardDimension = 5;

    public static void Main()
    {
      var input = ReadFromFile(FileLocation);
      var numbers = input.Item1;
      var boards = input.Item2;
      Print(input);

      var numOfBoardsRemoved = 0;
      var round = 1;
      while (boards.Any())
      {
        var calledNumber = CallNextNumber(numbers, boards);
        
        if (round > 4)
        {
          int boardIndex;
          while ((boardIndex = CheckIfBingo(boards)) > -1)
          {
            Console.WriteLine($"Board Nr.{boardIndex} Bingo");

            // winning board found continue with point evaluation
            var finalScore = CalculateScore(boards[boardIndex], calledNumber);

            Console.WriteLine($"Final score: {finalScore}\n");

            // break;
            // For second part remove won board and continue until only one board remains:
            boards.Remove(boardIndex);
            numOfBoardsRemoved++;

            if (boards.Count == 1)
            {
              //
            }
          }
        }
        round++;
      }

      Console.WriteLine($"Number of boards removed: {numOfBoardsRemoved}");
      Console.WriteLine($"Rounds played: {round}");
    }

    private static Tuple<Queue<int>, Dictionary<int, Cell[,]>> ReadFromFile(string fileLocation)
    {
      if (!File.Exists(FileLocation))
      {
        throw new FileNotFoundException("Could not load File at: " + FileLocation);
      }

      var numbers = new Queue<int>();
      Dictionary<int, Cell[,]> boards = new Dictionary<int, Cell[,]>();

      using (StreamReader sr = File.OpenText(FileLocation))
      {
        string firstLine = sr.ReadLine();

        if (firstLine != null)
        {
          firstLine.Split(',').Select(int.Parse).ToList().ForEach(num => numbers.Enqueue(num));
        }
        else
        {
          throw new FileLoadException("Could not read first line from file");
        }

        int boardNumber = 0;
        while ((sr.ReadLine()) != null)
        {
          var board = new Cell[BoardDimension, BoardDimension];

          for (int row = 0; row < BoardDimension; row++)
          {
            string s;
            if ((s = sr.ReadLine()) == null)
            {
              throw new FileLoadException("Could not read from file");
            }

            var values = Regex.Split(s.TrimStart(), @"\s+").Select(int.Parse).ToArray();

            for (int col = 0; col < BoardDimension; col++)
            {
              board[row, col] = new Cell(values[col]);
            }
          }

          boards.Add(boardNumber, board);
          boardNumber++;
        }
      }

      return Tuple.Create(numbers, boards);
    }

    private static void Print(Tuple<Queue<int>, Dictionary<int, Cell[,]>> input)
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
        for (int row = 0; row < BoardDimension; row++)
        {
          for (int col = 0; col < BoardDimension; col++)
          {
            Console.Write(board.Value[row, col]);
          }
          Console.Write("\n");
        }
        Console.Write("\n");
      }
    }

    private static int CallNextNumber(Queue<int> numbers, Dictionary<int, Cell[,]> boards)
    {
      var number = numbers.Dequeue();

      foreach (var board in boards)
      {
        SearchNumber(board.Value, number);
      }

      return number;
    }

    private static void SearchNumber(Cell[,] board, int number)
    {
      for (int row = 0; row < BoardDimension; row++)
      {
        for (int col = 0; col < BoardDimension; col++)
        {
          if (board[row, col].Number == number)
          {
            MarkCell(board[row, col]);
            return;
          }
        }
      }
    }

    private static void MarkCell(Cell cell)
    {
      cell.IsMarked = true;
    }

    private static int CheckIfBingo(Dictionary<int, Cell[,]> boards)
    {
      foreach (var board in boards)
      {
        int boardIndex = board.Key;
        var currentBoard = board.Value;

        // check rows
        for (int row = 0; row < BoardDimension; row++)
        {
          // check columns
          for (int col = 0; col < BoardDimension; col++)
          {
            if (!currentBoard[row, col].IsMarked)
            {
              break;
            }
            // if all cells marked -> Bingo Found!
            if (col == 4)
            {
              return boardIndex;
            }
          }
        }

        // check columns
        for (int col = 0; col < BoardDimension; col++)
        {
          // check rows
          for (int row = 0; row < BoardDimension; row++)
          {
            if (!currentBoard[row, col].IsMarked)
            {
              break;
            }
            // if all cells marked -> Bingo Found!
            if (row == 4)
            {
              return boardIndex;
            }
          }
        }

        // check diagonals

        // check falling diagonal
        for (int index = 0; index < BoardDimension; index++)
        {
          if (!currentBoard[index, index].IsMarked)
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
        for (int index = 0; index < BoardDimension; index++)
        {
          if (!currentBoard[index, BoardDimension-1 - index].IsMarked)
          {
            break;
          }
          // if all cells marked -> Bingo Found!
          if (index == 4)
          {
            return boardIndex;
          }
        }
      }

      return -1;
    }

    private static int CalculateScore(Cell[,] board, int calledNumber)
    {
      var currentScore = 0;
      for (int row = 0; row < BoardDimension; row++)
      {
        for (int col = 0; col < BoardDimension; col++)
        {
          if (board[row, col].IsMarked)
          {
            currentScore += board[row, col].Number;
          }
        }
      }

      return currentScore * calledNumber;
    }
  }
}
