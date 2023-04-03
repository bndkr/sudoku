using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolve
{
  public class SimulatedAnnealingSolver : ISudokuSolver
  {
    private static int INIT_TEMP = 1000;

    public Sudoku Solve(Sudoku initial, UpdateCallback callback)
    {
      var size = initial.GetSize();
      var possibleChars = initial.GetPossibleChars();
      var remainingNumsInRow = new List<List<char>>();

      // get all the initial numbers by column
      for (int col = 0; col < size; col++)
      {
        var colRollup = new List<char>();
        for (int row = 0; row < size; row++)
        {
          if (!initial.IsCellWriteable(col, row))
          {
            colRollup.Add(initial.GetCell(col, row));
          }
        }
        remainingNumsInRow.Add(colRollup);
      }

      for (int col = 0; col < size; col++)
      {
        foreach (char c in possibleChars)
        {
          if (!remainingNumsInRow[col].Contains(c))
          {
            // insert the char
            for (int row = 0; row < size; row++)
            {
              if (initial.IsCellWriteable(col, row) && 
                initial.GetCell(col, row) == Sudoku.EMPTY)
              {
                initial.SetCell(col, row, c, false);
                break;
              }
            }
          }
        }
      }

      // begin the simulated annealing algorithm

      Random r = new Random();
      double temp = INIT_TEMP;
      Sudoku curr = initial.Clone();
      int i = 0;
      while (temp > 0.1)
      {
        temp = Temperature(i);
        var neighbor = GenerateNeighbor(curr);
        if (r.NextDouble() < AcceptanceProbability(curr, neighbor, temp))
        {
          curr = neighbor;
          callback(curr, new Dictionary<string, string> { 
              { "Temperature", temp.ToString() },
              { "Current cost", GetCost(curr).ToString() }
            }, false);

          if (GetCost(curr) == 0)
            return curr;
        }
        i++;
      }
      callback(curr, new Dictionary<string, string> {
              { "Temperature", "0" },
              { "Current cost", GetCost(curr).ToString() }
            }, false);
      return initial;
    }

    private double Temperature(int timestep)
    {
      return INIT_TEMP * Math.Pow(1.1, -timestep / 10000);
    }

    private Sudoku GenerateNeighbor(Sudoku current)
    {
      Random r = new Random();
      Sudoku neighbor = current.Clone();
      int size = current.GetSize();

      // find two random readable cells in the same column
    restart:
      int colID = r.Next(size);
      int y1 = 0;
      int y2 = 0;
      while (true)
      {
        y1 = r.Next(size);
        if (current.IsCellWriteable(colID, y1))
          break;
      }
      // it is possible for there to only be one writeable cell
      // in the column. if we fail 40 times to find another 
      // writeable cell in the column, pick another column.
      int oops = 0;
      while (true)
      {
        y2 = r.Next(size);
        if (current.IsCellWriteable(colID, y2) && y1 != y2)
          break;
        else oops++;
        if (oops > 40) goto restart;
      }

      // swap the cells
      char temp = neighbor.GetCell(colID, y1);
      neighbor.SetCell(colID, y1, neighbor.GetCell(colID, y2), false);
      neighbor.SetCell(colID, y2, temp, false);
      return neighbor;
    }

    public int GetCost(Sudoku s)
    {
      int count = 0;
      int size = s.GetSize();
      for (int i = 0; i < size; i++)
      {
        for (int j = 0; j < size; j++)
        {
          if (s.CheckRowValid(j, i)) count++;
          if (s.CheckSquareValid(j, i)) count++;
        }
      }

      return count;
    }

    private double AcceptanceProbability(Sudoku currState, Sudoku candidateState, double temperature)
    {
      var currErrors = GetCost(currState);
      var candidateErrors = GetCost(candidateState);

      if (candidateErrors < currErrors)
      {
        return 1.0;
      }

      return Math.Exp(-(candidateErrors - currErrors) / temperature);
    }
  }
}
