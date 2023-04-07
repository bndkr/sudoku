using SudokuSolve;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
      Assert.IsTrue(BacktrackSolver.GetIncrementCell(s, 3, 0) == Sudoku.EMPTY);
    }

    public void UpdateCallback(Sudoku sudoku, Dictionary<string, string> processingData, bool done) { }

    [TestMethod]
    public void TestBacktrackSolve1()
    {
      string[] lines =
      {
        "9",
        "1 2 3 4 5 6 7 8 9",
        "4 9 - 1 3 6 7 - 8",
        "- 6 3 5 - - - 9 -",
        "5 - - - 2 9 3 6 4",
        "- 2 - 3 1 - - 4 -",
        "- 7 4 - - - 2 1 -",
        "- - 1 - 6 4 - 8 -",
        "1 8 6 9 - - - 2 5",
        "- 4 - - 5 1 8 3 -",
        "3 - 9 4 8 - - 7 -",
      };
      var sudoku = Sudoku.FromStrings(lines);
      var solver = new BacktrackSolver(sudoku, UpdateCallback);
      var result = solver.Solve().board;
      
      var s = new StringBuilder();
      s.AppendLine("4 9 2 1 3 6 7 5 8");
      s.AppendLine("7 6 3 5 4 8 1 9 2");
      s.AppendLine("5 1 8 7 2 9 3 6 4");
      s.AppendLine("8 2 5 3 1 7 9 4 6");
      s.AppendLine("6 7 4 8 9 5 2 1 3");
      s.AppendLine("9 3 1 2 6 4 5 8 7");
      s.AppendLine("1 8 6 9 7 3 4 2 5");
      s.AppendLine("2 4 7 6 5 1 8 3 9");
      s.AppendLine("3 5 9 4 8 2 6 7 1");
      string expected = s.ToString();

      var actual = result.ToString();
      Assert.IsTrue(expected == actual, $"{expected} is not equal to {actual}");
    }

    [TestMethod]
    public void TestIterators()
    {
      var sudoku = Sudoku.FromStrings(new string[]
        {
        "4",
        "1 2 3 4",
        "1 2 3 4",
        "4 3 2 1",
        "4 3 2 1",
        "1 2 3 4",
        });

      var expectedRows = new List<char[]>();
      expectedRows.Add(new char[4] { '1', '2', '3', '4' });
      expectedRows.Add(new char[4] { '4', '3', '2', '1' });
      expectedRows.Add(new char[4] { '4', '3', '2', '1' });
      expectedRows.Add(new char[4] { '1', '2', '3', '4' });

      var expectedCols = new List<char[]>();

      expectedCols.Add(new char[4] { '1', '4', '4', '1' });
      expectedCols.Add(new char[4] { '2', '3', '3', '2' });
      expectedCols.Add(new char[4] { '3', '2', '2', '3' });
      expectedCols.Add(new char[4] { '4', '1', '1', '4' });

      var expectedBoxs = new List<char[]>();
      expectedBoxs.Add(new char[4] { '1', '4', '2', '3' });
      expectedBoxs.Add(new char[4] { '3', '2', '4', '1' });
      expectedBoxs.Add(new char[4] { '4', '1', '3', '2' });
      expectedBoxs.Add(new char[4] { '2', '3', '1', '4' });

      for (int i = 0; i < 4; i++)
      {
        int idx = 0;
        foreach (var cell in sudoku.IterateRowCells(i))
        {
          Assert.IsTrue(cell.Read() == expectedRows[i][idx],
            $"row failure: {cell.Read()} != {expectedRows[i][idx]}");
          idx++;
        }
        Assert.IsTrue(idx == 4);
      }

      for (int i = 0; i < 4; i++)
      {
        int idx = 0;
        foreach (var cell in sudoku.IterateColCells(i))
        {
          Assert.IsTrue(cell.Read() == expectedCols[i][idx],
            $"col failure: {cell.Read()} != {expectedCols[i][idx]}");
          idx++;
        }
        Assert.IsTrue(idx == 4);
      }

      for (int i = 0; i < 4; i++)
      {
        int idx = 0;
        foreach (var cell in sudoku.IterateBox((i % 2) * 2, ((i / 2) * 2)))
        {
          // Assert.IsTrue(cell.Read() == expectedBoxs[i][idx],
          //   $"box failure[{idx}]: {cell.Read()} != {expectedBoxs[i][idx]}");
          idx++;
        }
        Assert.IsTrue(idx == 4);
      }
    }
    [TestMethod]
    public void TestCell()
    {
      Cell c = new Cell(new char[4] { '1', '2', '3', '4' });
      Assert.IsTrue(c.Read() == Sudoku.EMPTY);
      c.Write('1', false);
      Assert.IsTrue(c.GetCandidates().Count == 4);
      Assert.IsTrue(c.Read() == '1');
      c.Write('2', true);
      Assert.IsTrue(c.Read() == '2');
      c.Write('3', false); // not allowed because previous write operation made cell read-only
      Assert.IsTrue(c.Read() == '2');
      Assert.IsTrue(c.GetCandidates().Count == 0);

      Cell d = new Cell(new char[4] { '1', '2', '3', '4' });
      d.RemoveCandidate('4');
      Assert.IsTrue(d.GetCandidates().Count == 3);
      d.RemoveCandidate('3');
      Assert.IsTrue(d.GetCandidates().Count == 2);
      Assert.IsTrue(d.IsWriteable() == true);
      d.RemoveCandidate('2');
      Assert.IsTrue(d.GetCandidates().Count == 0);
      Assert.IsTrue(d.Read() == '1');
      Assert.IsTrue(d.IsWriteable() == false);
    }
    [TestMethod]
    public void TestBoxAgain()
    {
      var sudoku = Sudoku.FromStrings(
        new string[]
        { 
        "9",
        "1 2 3 4 5 6 7 8 9",
        "8 9 3 6 2 - - - 7",
        "- 5 1 7 - - - 4 9",
        "6 - - - - - - - -",
        "- 3 - - 6 5 4 2 -",
        "9 8 - - 4 - - 7 5",
        "- 4 2 8 1 - - 3 -",
        "- - - - - - - - 1",
        "3 1 - - - 6 8 9 -",
        "7 - - - 5 8 - - 4"
        });

      var expecteSudoku = Sudoku.FromStrings(
        new string[]
        {
          "9",
          "1 2 3 4 5 6 7 8 9",
          "8 9 3 6 2 4 5 1 7",
          "2 5 1 7 8 3 6 4 9",
          "6 7 4 5 9 1 2 8 3",
          "1 3 7 9 6 5 4 2 8",
          "9 8 6 3 4 2 1 7 5",
          "5 4 2 8 1 7 9 3 6",
          "4 6 8 2 3 9 7 5 1",
          "3 1 5 4 7 6 8 9 2",
          "7 2 9 1 5 8 3 6 4"});

      var solver = new BacktrackSolver(sudoku, UpdateCallback);
      sudoku = solver.Solve().board;
      Assert.IsTrue(sudoku.ToString() == expecteSudoku.ToString()); ;
    }
  }
}