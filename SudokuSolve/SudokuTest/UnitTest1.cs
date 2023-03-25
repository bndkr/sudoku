using SudokuSolve;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace SudokuTest
{
  [TestClass]
  public class SudokuTest
  {
    [TestMethod]
    public void TestSudoku()
    {
      {
        string[] lines =
        {
        "4",
        "1 2 3 4",
        "1 2 3 4",
        "4 3 2 1",
        "2 4 1 3",
        "3 1 4 2"
        };

        var s = Sudoku.FromStrings(lines);

        for (int i = 0; i < 4; i++)
        {
          for (int j = 0; j < 4; j++)
          {
            // the whole grid should be valid
            Assert.IsTrue(s.CheckCellValid(j, i), $"x: {j}, y:{i}");
          }
        }
      }
      {
        string[] lines =
        {
        "4",
        "1 2 3 4",
        "1 2 3 4",
        "4 3 2 1",
        "4 3 2 1",
        "1 2 3 4",
        };

        var s = Sudoku.FromStrings(lines);
        for (int i = 0; i < 4; i++)
        {
          for (int j = 0; j < 4; j++)
          {
            // the whole grid should be invalid
            Assert.IsFalse(s.CheckCellValid(j, i), $"x: {j}, y:{i}");
          }
        }
      }
      {
        string[] lines =
        {
        "4",
        "1 2 3 4",
        "- 2 - 4",
        "- 3 - 1",
        "- 3 - 1",
        "- 2 - 4",
        };

        var s = Sudoku.FromStrings(lines);
        for (int i = 0; i < 4; i++)
        {
          for (int j = 0; j < 4; j++)
          {
            // even values of x should be writeable
            if (j % 2 == 0)
            {
              Assert.IsTrue(s.IsCellWriteable(j, i));
            }
            else
            {
              Assert.IsFalse(s.IsCellWriteable(j, i));
            }
          }
        }
      }
    }

    [TestMethod]
    public void TestBacktracking()
    {
      string[] lines =
        {
        "4",
        "1 2 3 4",
        "1 2 3 4",
        "4 3 2 1",
        "4 3 2 1",
        "1 2 3 4",
        };

      var s = Sudoku.FromStrings(lines);
      Assert.IsTrue(BacktrackSolver.GetIncrementCell(s, 0, 0) == '2');
      Assert.IsTrue(BacktrackSolver.GetIncrementCell(s, 1, 0) == '3');
      Assert.IsTrue(BacktrackSolver.GetIncrementCell(s, 2, 0) == '4');
      Assert.IsTrue(BacktrackSolver.GetIncrementCell(s, 3, 0) == '\0');
    }
  }
}