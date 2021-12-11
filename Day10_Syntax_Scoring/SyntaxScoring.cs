using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10_Syntax_Scoring
{
  class SyntaxScoring
  {
    private IEnumerable<string> Input { get; } = File.ReadAllLines(@"D:\AoC\input.txt");

    private Stack<char> Brackets { get; }= new Stack<char>();

    private Queue<char> Errors { get; } = new Queue<char>();

    private Stack<char> OpenBrackets { get; } = new Stack<char>();

    private List<string> CorrectLines { get; } = new List<string>();

    private List<long> TotalScores { get; } = new List<long>();

    public SyntaxScoring()
    {
      foreach (var s in Input)
      {
        foreach (char c in s)
        {
          if (IsOpen(c))
          {
            Brackets.Push(c);
          }
          else
          {
            if (IsError(c))
            {
              Errors.Enqueue(c);
              Brackets.Clear();
              break;
            }

            Brackets.Pop();
          }
        }

        if (Brackets.Any())
        {
          CorrectLines.Add(new String(Brackets.ToArray()));
          Brackets.Clear();
        }
      }
    }

    public int GetSolution1()
    {
      var score = 0;
      while (Errors.Any())
      {
        switch (Errors.Dequeue())
        {
          case ')':
            score+=3;
            break;
          case ']':
            score+=57;
            break;
          case '}':
            score+=1197;
            break;
          case '>':
            score+=25137;
            break;
        }
      }

      return score;
    }

    public string GetSolution2()
    {
      foreach (var line in CorrectLines)
      {
        foreach (char c in line)
        {
          if (IsOpen(c))
          {
            Brackets.Push(c);
          }
          else
          {
            Brackets.Pop();
          }
        }

        TotalScores.Add(CalculateScore());
      }
      TotalScores.Sort();

      return TotalScores.OrderBy(x => x).ElementAt(TotalScores.Count / 2).ToString();
    }

    private long CalculateScore()
    {
      long score = 0;

      while (Brackets.Any())
      {
        OpenBrackets.Push(Brackets.Pop());
      }

      while (OpenBrackets.Any())
      {
        switch (OpenBrackets.Pop())
        {
          case '(':
            score = score * 5 + 1;
            break;
          case '[':
            score = score * 5 + 2;
            break;
          case '{':
            score = score * 5 + 3;
            break;
          case '<':
            score = score * 5 + 4;
            break;
        }
      }
      return score;
    }

    private bool IsError(char c)
    {
      switch (c)
      {
        case '}':
          if (Brackets.Peek().Equals('{')) return false;
          break;
        case ')':
          if (Brackets.Peek().Equals('(')) return false;
          break;
        case ']':
          if (Brackets.Peek().Equals('[')) return false;
          break;
        case '>':
          if (Brackets.Peek().Equals('<')) return false;
          break;
      }
      return true;
    }

    private static bool IsOpen(char c)
    {
      return c.Equals('{') || c.Equals('(') || c.Equals('[') || c.Equals('<');
    }
  }
}
