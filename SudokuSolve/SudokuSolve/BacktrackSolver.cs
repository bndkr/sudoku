using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SudokuSolve
{
  public class BacktrackSolver : ISudokuSolver
  {
    public Sudoku Solve(Sudoku puzzle, UpdateCallback callback)
    {
      int idx = 0;
      int size = puzzle.GetSize();
      var possibleChrs = puzzle.GetPossibleChars();

      int numCells = size * size;

      bool backtracking = false;

      while (idx >= 0 && idx < size * size)
      {
        int x = idx % size;
        int y = idx / size;

        if (puzzle.IsCellWriteable(x, y))
        {
          if (puzzle.GetCell(x, y) == Sudoku.EMPTY)
            puzzle.SetCell(x, y, possibleChrs[0], false);

          if (puzzle.CheckCellValid(x, y) && !backtracking)
          {
            idx++;
          }
          else
          {
            // increment it until it's valid or it overflows
            char incremented = puzzle.GetCell(x, y);
            bool valid = false;
            bool overflowed = false;
            while (!valid && !overflowed)
            {
              incremented = GetIncrementCell(puzzle, x, y);
              if (incremented == Sudoku.EMPTY)
              {
                overflowed = true;
                break;
              }
              puzzle.SetCell(x, y, incremented, false);
              valid = puzzle.CheckCellValid(x, y);
            }

            if (overflowed)
            {
              idx--;
              puzzle.SetCell(x, y, Sudoku.EMPTY, false);
              backtracking = true;
              continue;
            }
            else if (valid)
            {
              backtracking = false;
              idx++;
              continue;
            }
            else
            {
              throw new Exception("not sure what to do here");
            }
          }
        }
        else
        {
          if (backtracking) idx--;
          else idx++;
        }
        callback(puzzle, new Dictionary<string, string> { { "index", idx.ToString() } }, false);
      }
      callback(puzzle, new Dictionary<string, string> { }, true);
      return puzzle;
    }

    public static char GetIncrementCell(Sudoku s, int x, int y)
    {
      char[] possibleChars = s.GetPossibleChars();
      int size = s.GetSize();
      char cell = s.GetCell(x, y);
      if (cell == Sudoku.EMPTY)
        return possibleChars[0];

      for (int i = 0; i < size; i++)
      {
        if (possibleChars[i] == cell)
        {
          if (i == size - 1)
          {
            return Sudoku.EMPTY;
          }
          return possibleChars[i + 1];
        }
      }
      throw new Exception("char is invalid");
    }
  }
}
