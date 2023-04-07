using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolve
{
  // this should be called anytime a solver updates the grid state
  // so it can be displayed to the user as the alogorithm runs
  public delegate void UpdateCallback(Sudoku sudoku, Dictionary<string, string> processingData, bool done);

  public struct SudokuSolveResult
  {
    public bool success;
    public Sudoku board;
  }

  public abstract class SudokuSolver
  {
    protected Sudoku board;
    protected UpdateCallback callback;
    public SudokuSolver(Sudoku board, UpdateCallback callback)
    {
      this.board = board;
      this.callback = callback;
    }

    public SudokuSolveResult Solve()
    {
      // SolveNakedSingles();
      SolveByGuessing();
      return new SudokuSolveResult { success = board.IsSolved(), board = board };
    }
    public abstract void SolveNakedSingles();

    public abstract void SolveByGuessing();
  }
}
