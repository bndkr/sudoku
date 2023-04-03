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

  public interface ISudokuSolver
  {
    Sudoku Solve(Sudoku initial, UpdateCallback callback);
  }

  public abstract class SudokuCellSolver : ISudokuSolver
  {

    Sudoku board;
    SudokuCellSolver(Sudoku board)
    {
      this.board = board;
    }

    public Sudoku Solve(Sudoku initial, UpdateCallback callback)
    {
      throw new NotImplementedException();
    }

    public abstract void SolveNakedSingles();


  }
}
