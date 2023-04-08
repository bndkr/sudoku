using System;
using System.Collections.Generic;

namespace SudokuSolve
{
  public class BacktrackSolver : SudokuSolver
  {
    public BacktrackSolver(Sudoku board, UpdateCallback callback) : base(board, callback) { }
    public static char GetIncrementCell(Sudoku s, int x, int y)
    {
      char[] possibleChars = s.GetPossibleChars();
      int size = s.GetSize();
      char cell = s.GetCellValue(x, y);
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

    public override void SolveNakedSingles()
    {
      int size = board.GetSize();
      for (int col = 0; col < size; col++)
      {
        for (int row = 0; row < size; row++)
        {
          if (!board.IsCellWriteable(col, row) && board.GetCellValue(col, row) != Sudoku.EMPTY)
          {
            char value = board.GetCellValue(col, row);

            // remove candidates from the same row
            foreach (var cell in board.IterateRowCells(row))
            {
              cell.RemoveCandidate(value);
            }

            // remove candidates from the same col
            foreach (var cell in board.IterateColCells(col))
            {
              cell.RemoveCandidate(value);
            }

            // remove candidates from the same box
            foreach (var cell in board.IterateBox(col, row))
            {
              cell.RemoveCandidate(value);
            }
            callback(board, new Dictionary<string, string> { }, false);
          }
        }
      }
      callback(board, new Dictionary<string, string> { }, false);
    }

    public override void SolveNakedPairs()
    {
      int size = board.GetSize();
      for (int col = 0; col < size; col++)
      {
        for (int row = 0; row < size; row++)
        {
          if (board.IsCellWriteable(col, row))
          {
            var candidates = board.GetCellCandidates(col, row);
            var curr = board.GetCell(col, row);

            if (candidates.Count == 2)
            {
              char candidate1 = candidates[0];
              char candidate2 = candidates[1];

              // look for other cells in the same row with those
              // exact same 2 candidates
              foreach (var cell in board.IterateRowCells(row))
              {
                if (cell != curr)
                {
                  var otherCandidates = cell.GetCandidates();
                  if (otherCandidates.Count == 2 &&
                    otherCandidates.Contains(candidate1) &&
                    otherCandidates.Contains(candidate2))
                  {
                    // remove candidates from other cells in the row
                    foreach (var otherCell in board.IterateRowCells(row))
                    {
                      if (otherCell != cell && otherCell != curr)
                      {
                        otherCell.RemoveCandidate(candidate1);
                        otherCell.RemoveCandidate(candidate2);
                      }
                    }
                  }
                }
              }

              foreach (var cell in board.IterateColCells(col))
              {
                if (cell != curr)
                {
                  var otherCandidates = cell.GetCandidates();
                  if (otherCandidates.Count == 2 &&
                    otherCandidates.Contains(candidate1) &&
                    otherCandidates.Contains(candidate2))
                  {
                    // remove candidates from other cells in the col
                    foreach (var otherCell in board.IterateColCells(col))
                    {
                      if (otherCell != cell && otherCell != curr)
                      {
                        otherCell.RemoveCandidate(candidate1);
                        otherCell.RemoveCandidate(candidate2);
                      }
                    }
                  }
                }
              }

              foreach (var cell in board.IterateBox(col, row))
              {
                if (cell != curr)
                {
                  var otherCandidates = cell.GetCandidates();
                  if (otherCandidates.Count == 2 &&
                    otherCandidates.Contains(candidate1) &&
                    otherCandidates.Contains(candidate2))
                  {
                    // remove candidates from other cells in the box
                    foreach (var otherCell in board.IterateBox(col, row))
                    {
                      if (otherCell != cell && otherCell != curr)
                      {
                        otherCell.RemoveCandidate(candidate1);
                        otherCell.RemoveCandidate(candidate2);
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    public override void SolveByGuessing()
    {
      int idx = 0;
      int size = board.GetSize();
      var possibleChrs = board.GetPossibleChars();

      int numCells = size * size;

      bool backtracking = false;

      while (idx >= 0 && idx < size * size)
      {
        int x = idx % size;
        int y = idx / size;

        if (board.IsCellWriteable(x, y))
        {
          if (board.GetCellValue(x, y) == Sudoku.EMPTY)
            board.SetCell(x, y, possibleChrs[0], false);

          if (board.CheckCellValid(x, y) && !backtracking)
          {
            idx++;
          }
          else
          {
            // increment it until it's valid or it overflows
            char incremented = board.GetCellValue(x, y);
            bool valid = false;
            bool overflowed = false;
            while (!valid && !overflowed)
            {
              incremented = GetIncrementCell(board, x, y);
              if (incremented == Sudoku.EMPTY)
              {
                overflowed = true;
                break;
              }
              board.SetCell(x, y, incremented, false);
              valid = board.CheckCellValid(x, y);
            }

            if (overflowed)
            {
              idx--;
              board.SetCell(x, y, Sudoku.EMPTY, false);
              backtracking = true;
              continue;
            }
            else // (valid)
            {
              backtracking = false;
              idx++;
              continue;
            }
          }
        }
        else
        {
          if (backtracking) idx--;
          else idx++;
        }
        callback(board, new Dictionary<string, string> { { "index", idx.ToString() } }, false);
      }
      callback(board, new Dictionary<string, string> { }, true);
    }
  }
}
