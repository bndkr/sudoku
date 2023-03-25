using SudokuSolve;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
    }
  }
}