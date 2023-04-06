using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SudokuSolve
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    private void loadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      openFileDialog1.ShowDialog();
      try
      {
        currentSudoku = Sudoku.FromFile(openFileDialog1.FileName);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Cannot open sudoku file: " + ex.Message);
      }

      if (currentSudoku != null)
      {
        sudokuDisplay.Text = currentSudoku.ToString();
      }
    }

    private Sudoku currentSudoku = null;

    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {
    }

    private void sudokuDisplay_TextChanged(object sender, EventArgs e)
    {
    }

    private async void button1_Click(object sender, EventArgs e)
    {
      if (currentSudoku == null) return;

      SudokuSolver solver = new BacktrackSolver(currentSudoku, Callback);

      var task = await Task.Run(() => solver.Solve());
    }

    private void Callback(Sudoku sudoku, Dictionary<string, string> data, bool done)
    {
      updateSudokuText = sudoku.ToString();
      updateData = data;
      updatedone = done;
    }

    private string updateSudokuText = null;
    private Dictionary<string, string> updateData = null;
    private bool updatedone = false;

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (updatedone)
      {
        if (!currentSudoku.IsSolved())
        {
          MessageBox.Show("Could not solve sudoku.");
        }
        updatedone = false;
      }
        
      if (updateSudokuText != null)
      {
        sudokuDisplay.Text = updateSudokuText;
        updateSudokuText = null;
      }
      if (updateData != null)
      {
        var str = new StringBuilder();

        foreach (var pair in updateData)
        {
          str.AppendLine($"{pair.Key}: {pair.Value}");
        }
        dataBox.Text = str.ToString();
        updateData = null;
      }
    }
  }
}
