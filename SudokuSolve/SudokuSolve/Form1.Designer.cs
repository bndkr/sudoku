namespace SudokuSolve
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newSudokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.sudokuDisplay = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.radioBacktracking = new System.Windows.Forms.RadioButton();
      this.radioSimulatedAnnealing = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(550, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSudokuToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // newSudokuToolStripMenuItem
      // 
      this.newSudokuToolStripMenuItem.Name = "newSudokuToolStripMenuItem";
      this.newSudokuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.newSudokuToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
      this.newSudokuToolStripMenuItem.Text = "New Sudoku";
      // 
      // loadToolStripMenuItem
      // 
      this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
      this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.loadToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
      this.loadToolStripMenuItem.Text = "Load";
      this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
      this.saveToolStripMenuItem.Text = "Save";
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "txt";
      this.openFileDialog1.FileName = "openFileDialog1";
      this.openFileDialog1.Title = "Open Sudoku File";
      this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
      // 
      // sudokuDisplay
      // 
      this.sudokuDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.sudokuDisplay.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.sudokuDisplay.Location = new System.Drawing.Point(168, 37);
      this.sudokuDisplay.Multiline = true;
      this.sudokuDisplay.Name = "sudokuDisplay";
      this.sudokuDisplay.ReadOnly = true;
      this.sudokuDisplay.Size = new System.Drawing.Size(349, 330);
      this.sudokuDisplay.TabIndex = 1;
      this.sudokuDisplay.WordWrap = false;
      this.sudokuDisplay.TextChanged += new System.EventHandler(this.sudokuDisplay_TextChanged);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 107);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 2;
      this.button1.Text = "Solve";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // radioBacktracking
      // 
      this.radioBacktracking.AutoSize = true;
      this.radioBacktracking.Location = new System.Drawing.Point(13, 53);
      this.radioBacktracking.Name = "radioBacktracking";
      this.radioBacktracking.Size = new System.Drawing.Size(88, 17);
      this.radioBacktracking.TabIndex = 3;
      this.radioBacktracking.TabStop = true;
      this.radioBacktracking.Text = "Backtracking";
      this.radioBacktracking.UseVisualStyleBackColor = true;
      // 
      // radioSimulatedAnnealing
      // 
      this.radioSimulatedAnnealing.AutoSize = true;
      this.radioSimulatedAnnealing.Location = new System.Drawing.Point(13, 76);
      this.radioSimulatedAnnealing.Name = "radioSimulatedAnnealing";
      this.radioSimulatedAnnealing.Size = new System.Drawing.Size(121, 17);
      this.radioSimulatedAnnealing.TabIndex = 4;
      this.radioSimulatedAnnealing.TabStop = true;
      this.radioSimulatedAnnealing.Text = "Simulated Annealing";
      this.radioSimulatedAnnealing.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 155);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "label1";
      this.label1.Click += new System.EventHandler(this.label1_Click);
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Interval = 16;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(550, 395);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.radioSimulatedAnnealing);
      this.Controls.Add(this.radioBacktracking);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.sudokuDisplay);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "Sudoku Solver";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newSudokuToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.TextBox sudokuDisplay;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.RadioButton radioBacktracking;
    private System.Windows.Forms.RadioButton radioSimulatedAnnealing;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Timer timer1;
  }
}

