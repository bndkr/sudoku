using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolve
{
  public class Sudoku
  {
    public static char EMPTY = '-';
    private Sudoku(int size, char[] possibleChars) // size is expected to be 4, 9, 16, or 25
    {
      m_grid = new char[size * size];
      m_mask = new bool[size * size];
      m_size = size;

      if (possibleChars == null)
        throw new ArgumentNullException("possibleChars cannot be null");
      if (possibleChars.Length != size)
        throw new ArgumentException($"possibleChars must be length {size}");

      m_possibleChars = possibleChars;

      if ((int)Math.Sqrt(size) * (int)Math.Sqrt(size) != size)
        throw new Exception("size must be a square");

      for (int x = 0; x < size; x++)
      {
        for (int y = 0; y < size; y++)
        {
          SetCell(x, y, EMPTY);
          SetMask(x, y, true); // grid will initially be all writable
        }
      }
      m_possibleChars = possibleChars;
    }

    public bool IsEmpty(int x, int y)
    {
      return GetCell(x, y) == EMPTY;
    }

    public char[] GetPossibleChars()
    {
      return m_possibleChars;
    }

    public char GetCell(int x, int y)
    {
      checkCellBoundaries(x, y);

      int index = x + (y * m_size);
      return m_grid[index];
    }

    public int GetSize()
    {
      return m_size;
    }

    public bool IsCellWriteable(int x, int y)
    {
      checkCellBoundaries(x, y);

      int index = x + (y * m_size);
      return m_mask[index];
    }

    public Sudoku Clone()
    {
      var newPossibleChars = new char[m_size];
      for (int i = 0; i < m_size; i++)
      {
        newPossibleChars[i] = m_possibleChars[i];
      }

      var s = new Sudoku(m_size, newPossibleChars);

      for (int x = 0; x < m_size; x++)
      {
        for (int y = 0; y < m_size; y++)
        {
          s.SetCell(x, y, GetCell(x, y));
          s.SetMask(x, y, IsCellWriteable(x, y));
        }
      }
      return s;
    }

    public bool SetCell(int x, int y, char value)
    {
      checkCellBoundaries(x, y);

      if (m_possibleChars.Any(c => c == value) || value == EMPTY)
      {
        if (IsCellWriteable(x, y))
        {
          int index = x + (y * m_size);
          m_grid[index] = value;
          return true;
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
          str.Append(" ");
        }
        str.AppendLine();
      }
      return str.ToString();
    }

    public bool CheckCellValid(int x, int y)
    {
      return CheckRowValid(x, y) && CheckColValid(x, y) && CheckSquareValid(x, y);
    }

    private void checkCellBoundaries(int x, int y)
    {
      if (x >= m_size || y >= m_size)
      {
        throw new Exception($"invalid indices: x:{x}, y:{y}");
      }
    }
    private void SetMask(int x, int y, bool mask)
    {
      checkCellBoundaries(x, y);
      int index = x + (y * m_size);
      m_mask[index] = mask;
    }

    private bool CheckRowValid(int x, int y)
    {
      // check for matches in the row
      var value = GetCell(x, y);
      for (int i = 0; i < m_size; i++)
      {
        if (i != x && GetCell(i, y) == value)
        {
          return false;
        }
      }
      return true;
    }

    private bool CheckColValid(int x, int y)
    {
      // check for matches in the column
      var value = GetCell(x, y);
      for (int i = 0; i < m_size; i++)
      {
        if (i != y && GetCell(x, i) == value)
        {
          return false;
        }
      }
      return true;
    }

    private bool CheckSquareValid(int x, int y)
    {
      // check for matches in the local square
      var value = GetCell(x, y);
      int squareSide = (int) System.Math.Sqrt(m_size);
      int squareCornerX = x / squareSide;
      int squareCornerY = y / squareSide;

      for (int i = squareCornerX * squareSide; i < squareSide; i++)
      {
        for (int j = squareCornerY * squareSide; j < squareSide; j++)
        {
          if (!(x == i && y == j) && GetCell(i, j) == value)
          {
            return false;
          }
        }
      }
      return true;
    }
    private char[] m_possibleChars;
    private char[] m_grid; 
    private bool[] m_mask; // false means unwritable, true means writeable
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
            sudoku.SetMask(j, i, true);
            sudoku.SetCell(j, i, EMPTY);
          }
          else
          {
            sudoku.SetCell(j, i, row[j][0]);
            sudoku.SetMask(j, i, false); // mark as read-only
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
