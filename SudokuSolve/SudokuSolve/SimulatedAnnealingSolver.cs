using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolve
{
  public class SimulatedAnnealingSolver : ISudokuSolver
  {

    private static int MAX_STEPS = 10000;
    private static int INIT_TEMP = 100;

    public Sudoku Solve(Sudoku initial, UpdateCallback callback)
    {
      var size = initial.GetSize();
      var possibleChars = initial.GetPossibleChars();
      var remainingNums = new int[size];

      // count how many of each char the board has
      for (int i = 0; i < size; i++) // for each possible char
      {
        remainingNums[i] = size;
        for (int x = 0; x < size; x++) // for each grid square
        {
          for (int y = 0; y < size; y++)
          {
            if (initial.GetCell(x, y) == possibleChars[i])
            {
              remainingNums[i]--;
            }
          }
        }
      }

      // populate the board with the missing numbers
      for (int x = 0; x < size; x++)
      {
        for (int y = 0; y < size; y++)
        {
          if (initial.GetCell(x, y) == Sudoku.EMPTY)
          {
            for (int i = 0; i < size; i++)
            {
              if (remainingNums[i] > 0)
              {
                initial.SetCell(x, y, possibleChars[i]);
                remainingNums[i]--;
                break;
              }
            }
          }
        }
      }

      // begin the simulated annealing algorithm

      Random r = new Random();
      double temp;
      Sudoku curr = initial.Clone();
      for (int i = 0; i < MAX_STEPS; i++)
      {
        temp = Temperature(i);
        var neighbor = GenerateNeighbor(curr);
        if (r.NextDouble() < AcceptanceProbability(curr, neighbor, temp))
        {
          curr = neighbor;
          callback(curr, temp);
          if (CountErrors(curr) == 0)
            return curr;
        }
      }
      return curr;
    }

    private double Temperature(int timestep)
    {
      return INIT_TEMP - ((double) timestep / MAX_STEPS) * INIT_TEMP;
    }

    private Sudoku GenerateNeighbor(Sudoku current)
    {
      Random r = new Random();
      Sudoku neighbor = current.Clone();
      int size = current.GetSize();

      // find two random readable cells
      int x1 = 0;
      int y1 = 0;
      int x2 = 0;
      int y2 = 0;
      bool found1 = false;
      bool found2 = false;
      while (!(found1 && found2))
      {
        if (!found1)
        {
          x1 = r.Next(size);
          y1 = r.Next(size);
          if (neighbor.IsCellWriteable(x1, y1) &&
            !(x2 == x1 && y2 == y1))
            found1 = true;
        }
        if (!found2)
        {
          x2 = r.Next(size);
          y2 = r.Next(size);
          if (neighbor.IsCellWriteable(x2, y2) &&
            !(x2 == x1 && y2 == y1))
            found2 = true;
        }
      }
      // swap the cells
      char temp = neighbor.GetCell(x1, y1);
      neighbor.SetCell(x1, y1, neighbor.GetCell(x2, y2));
      neighbor.SetCell(x2, y2, temp);
      return neighbor;
    }

    private int CountErrors(Sudoku s)
    {
      int count = 0;
      var size = s.GetSize();
      for (int i = 0; i < size; i++)
      {
        for (int j = 0; j < size; j++)
        {
          if (!s.CheckCellValid(i, j))
            count++;
        }
      }
      return count;
    }

    private double AcceptanceProbability(Sudoku currState, Sudoku candidateState, double temperature)
    {
      var currErrors = CountErrors(currState);
      var candidateErrors = CountErrors(candidateState);

      if (candidateErrors < currErrors)
      {
        return 1.0;
      }

      return Math.Exp(-(candidateErrors - currErrors) / temperature);
    }
  }
}
