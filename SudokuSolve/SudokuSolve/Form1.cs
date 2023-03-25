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

      ISudokuSolver solver;
      if (radioBacktracking.Checked)
        solver = new BacktrackSolver();
      else if (radioSimulatedAnnealing.Checked)
        solver = new SimulatedAnnealingSolver();
      else
        throw new Exception("unrecognized solving algorithm");

      // TODO: have this work on a seperate thread

      var task = await Task.Run(() => solver.Solve(currentSudoku, Callback));
      currentSudoku = solver.Solve(currentSudoku, Callback);
      sudokuDisplay.Text = currentSudoku.ToString();
    }

    private void Callback(Sudoku sudoku, double progress)
    {
      invalid = sudoku.ToString();
    }

    private string invalid = null;
    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (invalid != null)
      {
        sudokuDisplay.Text = invalid;
        invalid = null;
      }
    }
  }
}
