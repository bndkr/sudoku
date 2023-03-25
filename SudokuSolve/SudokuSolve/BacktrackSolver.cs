using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolve
{
  public class BacktrackSolver : ISudokuSolver
  {
    public Sudoku Solve(Sudoku puzzle, UpdateCallback callback)
    {
      int idx = 0;
      int size = puzzle.GetSize();
      int numCells = size * size;
      var possibleValues = puzzle.GetPossibleChars();
      bool backtracking = false;

      while (idx < numCells && idx >= 0)
      {
        here:
        int x = idx % size;
        int y = idx / size;
        char val = puzzle.GetCell(x, y);
        bool writeable = puzzle.IsCellWriteable(x, y);

        if (writeable)
        {
          if (backtracking)
          {
            // increment the current value
            for (int i = 0; i < possibleValues.Length; i++)
            {
              if (possibleValues[i] == val)
              {
                // if it's the last possible value, backtrack
                if (i == possibleValues.Length - 1)
                {
                  backtracking = true;
                  puzzle.SetCell(x, y, Sudoku.EMPTY);
                  callback(puzzle, 0);
                  idx--;
                  goto here;
                }
                else
                {
                  puzzle.SetCell(x, y, possibleValues[i + 1]);
                  callback(puzzle, 0);
                }
              }
            }
          }
          if (val == Sudoku.EMPTY)
          {
            puzzle.SetCell(x, y, possibleValues[0]);
            val = possibleValues[0];
            callback(puzzle, 0);
          }
          if (puzzle.CheckCellValid(x, y))
          {
            backtracking = false;
            idx++;
          }
          else
          {
            for (int i = 0; i < possibleValues.Length; i++)
            {
              if (possibleValues[i] == val)
              {
                // if it's the last possible value, backtrack
                if (i == possibleValues.Length - 1)
                {
                  backtracking = true;
                  puzzle.SetCell(x, y, Sudoku.EMPTY);
                  idx--;
                }
                else
                {
                  puzzle.SetCell(x, y, possibleValues[i + 1]);
                }
              }
            }
          }
        }
        else
        {
          if (backtracking) idx--;
          else idx++;
        }
      }

      return puzzle;
    }
  }
}
