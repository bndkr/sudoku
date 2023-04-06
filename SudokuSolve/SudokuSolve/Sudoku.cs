using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolve
{
  public class Cell
  {
    private char[] possibleChars;
    private bool writeable;
    private List<char> candidates;
    private char value;

    public Cell(char[] possibleChars)
    {
      this.possibleChars = possibleChars;
      this.value = Sudoku.EMPTY;
      this.writeable = true;
      CandidateInit();
    }
    public Cell(char[] possibleChars, char value)
    {
      Debug.Assert(value != Sudoku.EMPTY);
      this.possibleChars = possibleChars;
      this.value = value;
      this.writeable = false;
      CandidateInit();
    }

    public List<char> GetCandidates() { return candidates; }

    private void CandidateInit()
    {
      this.candidates = new List<char>();
      foreach (var c in possibleChars)
      {
        candidates.Add(c);
      }
    }
    /* Writes a value to the cell. If solved = true, indicates the new 
     * value is fixed and cannot be changed, otherwise the cell remains
     * mutable.
     */
    public bool Write(char toWrite, bool solved)
    {
      if ((possibleChars.Any(c => c == toWrite) || toWrite == Sudoku.EMPTY) && writeable)
      {
        value = toWrite;
        if (solved)
        {
          writeable = false;
          candidates.Clear();
        }
          return true;
      }
      return false;
    }

    public bool IsWriteable()
    {
      return writeable;
    }

    public override string ToString()
    {
      return value.ToString();
    }

    public char Read()
    {
      return value;
    }

    public Cell Clone()
    {
      // NOTE: resets the candidate values
      Cell result;
      if (writeable)
      {
        result = new Cell(possibleChars);
      }
      else
      {
        result = new Cell(possibleChars, value);
      }
      return result;
    }

    /* Removes the specified candidate from the cell.
     * Returns true if after removal, only one candidate remains
     * and the cell is populated with that value. Returns false
     * otherwise.
     */
    public bool RemoveCandidate(char candidate)
    {
      if (!candidates.Contains(candidate)) return false;
      candidates.Remove(candidate);
      if (candidates.Count == 1)
      {
        Write(candidates[0], true); // last available solution
        return true;
      }
      return false;
    }
  }

  public class Sudoku
  {
    public static char EMPTY = '-';
    private Sudoku(int size, char[] possibleChars) 
    {
      // size is expected to be 4, 9, 16, or 25
      if ((int)Math.Sqrt(size) * (int)Math.Sqrt(size) != size)
        throw new Exception("size must be a square");

      if (possibleChars == null)
        throw new ArgumentNullException("possibleChars cannot be null");
      if (possibleChars.Length != size)
        throw new ArgumentException($"possibleChars must be length {size}");

      m_size = size;
      m_possibleChars = possibleChars;
      m_grid = new Cell[size * size];

      for (int i = 0; i < size * size; i++)
      {
        m_grid[i] = new Cell(possibleChars); // grid is initialized all blank
      }
    }

    private int GetIndex(int x, int y)
    {
      return x + (y * m_size);
    }

 
    public char[] GetPossibleChars()
    {
      return m_possibleChars;
    }

    public char GetCell(int x, int y)
    {
      checkCellBoundaries(x, y);

      int index = GetIndex(x, y);
      return m_grid[index].Read();
    }

    public int GetSize()
    {
      return m_size;
    }

    public bool IsCellWriteable(int x, int y)
    {
      checkCellBoundaries(x, y);

      int index = GetIndex(x, y);
      return m_grid[index].IsWriteable();
    }

    public bool RemoveCandidate(int x, int y, char candidate)
    {
      checkCellBoundaries(x, y);

      int index = GetIndex(x, y);
      return m_grid[index].RemoveCandidate(candidate);
    }

    public Sudoku Clone()
    {
      var newPossibleChars = new char[m_size];
      for (int i = 0; i < m_size; i++)
      {
        newPossibleChars[i] = m_possibleChars[i];
      }

      var s = new Sudoku(m_size, newPossibleChars);

      for (int i = 0; i < m_size * m_size; i++)
      {
        m_grid[i] = s.m_grid[i].Clone();
      }
      return s;
    }

    /* Sets the specified cell with a value. If this is the 
     * final state of the cell, pass 'true' for solved, false
     * otherwise. Returns success.
     */
    public bool SetCell(int x, int y, char value, bool solved)
    {
      checkCellBoundaries(x, y);

      if (m_possibleChars.Any(c => c == value) || value == EMPTY)
      {
        if (IsCellWriteable(x, y))
        {
          int index = GetIndex(x, y);
          return m_grid[index].Write(value, solved);
        }
      }
      else
      {
        throw new Exception($"char {value} is not allowed");
      }
      return false;
    }

    public override string ToString()
    {
      var str = new StringBuilder();
      for (int i = 0; i < m_size; i++)
      {
        for (int j = 0; j < m_size; j++)
        {
          str.Append(GetCell(j, i));
          if (j != m_size - 1) str.Append(" ");
        }
        str.AppendLine();
      }
      return str.ToString();
    }

    public bool CheckCellValid(int x, int y)
    {
      if (GetCell(x, y) == EMPTY) return false;

      return CheckRowValid(x, y) && CheckColValid(x, y) && CheckSquareValid(x, y);
    }

    private void checkCellBoundaries(int x, int y)
    {
      if (x >= m_size || y >= m_size)
      {
        throw new Exception($"invalid indices for {m_size} x {m_size} grid: x:{x}, y:{y}");
      }
    }

    public bool CheckRowValid(int x, int y)
    {
      // check for matches in the row
      var value = GetCell(x, y);
      if (value == EMPTY) return false;
      int matches = 0;
      foreach (Cell c in IterateRowCells(y))
      {
        if (value == c.Read()) matches++;
      }
      return matches == 1;
    }
    public bool CheckColValid(int x, int y)
    {
      // check for matches in the column
      var value = GetCell(x, y);
      if (value == EMPTY) return false;
      int matches = 0;
      foreach (Cell c in IterateColCells(x))
      {
        if (value == c.Read()) matches++;
      }
      return matches == 1;
    }
    public bool CheckSquareValid(int x, int y)
    {
      // check for matches in the local square
      var value = GetCell(x, y);
      if (value == EMPTY) return false;
      int matches = 0;
      foreach (Cell c in IterateBox(x, y))
      {
        char v = c.Read();
        if (value == v) matches++;
      }
      // TODO: investigate why 'matches' can be 0 and be valid
      return matches < 2;
    }

    public bool IsSolved()
    {
      for (int i = 0; i < m_size; i++)
      {
        for (int j = 0; j < m_size; j++)
        {
          if (m_grid[i + (j * m_size)].Read() == EMPTY) return false;
          if (!CheckSquareValid(i, j)) return false;
          if (!CheckRowValid(i, j)) return false;
          if (!CheckColValid(i, j)) return false;
        }
      }
      return true;
    }
    public IEnumerable<Cell> IterateRowCells(int y)
    {
      for (int i = 0; i < m_size; i++)
      {
        yield return m_grid[GetIndex(i, y)];
      }
    }

    public IEnumerable<Cell> IterateColCells(int x)
    {
      for (int i = 0; i < m_size; i++)
      {
        yield return m_grid[GetIndex(x, i)];
      }
    }

    public IEnumerable<Cell> IterateBox(int x, int y)
    {
      int squareSide = (int)System.Math.Sqrt(m_size);
      int squareCornerX = x / squareSide;
      int squareCornerY = y / squareSide;

      for (int i = squareCornerX * squareSide; i < squareSide; i++)
      {
        for (int j = squareCornerY * squareSide; j < squareSide; j++)
        {
          yield return m_grid[GetIndex(i, j)];
        }
      }
    }

    private char[] m_possibleChars;
    private Cell[] m_grid; 
    private int m_size;

    // factory methods
    public static Sudoku FromStrings(string[] lines)
    {
      if (lines.Length == 0)
        throw new Exception("empty file");

      var size = int.Parse(lines[0]);

      if (lines.Length < size + 2)
        throw new Exception("invalid sudoku file");

      var chars = lines[1].Split(' ');

      if (chars.Length < size)
        throw new Exception("invalid number of possible chars");

      var possibleChars = new char[size];
      for (int i = 0; i < chars.Length; i++)
      {
        possibleChars[i] = chars[i][0];
      }

      var sudoku = new Sudoku(size, possibleChars);

      for (int i = 0; i < size; i++)
      {
        var row = lines[i + 2].Split(' ');
        for (int j = 0; j < size; j++)
        {
          if (row[j][0] == EMPTY)
          {
            sudoku.SetCell(j, i, EMPTY, false);
          }
          else
          {
            sudoku.SetCell(j, i, row[j][0], true);
          }
        }
      }
      return sudoku;
    }
    public static Sudoku FromFile(string filename)
    {
      if (!System.IO.File.Exists(filename))
      {
        return null;
      }
      var lines = System.IO.File.ReadAllLines(filename);
      return FromStrings(lines);
    }
  }
}
