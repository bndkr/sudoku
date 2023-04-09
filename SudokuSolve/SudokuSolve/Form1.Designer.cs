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
      this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.sudokuDisplay = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.dataBox = new System.Windows.Forms.TextBox();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
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
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
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
      this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
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
      this.button1.Location = new System.Drawing.Point(12, 37);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(67, 23);
      this.button1.TabIndex = 2;
      this.button1.Text = "Solve";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Interval = 16;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // dataBox
      // 
      this.dataBox.Location = new System.Drawing.Point(12, 258);
      this.dataBox.Multiline = true;
      this.dataBox.Name = "dataBox";
      this.dataBox.Size = new System.Drawing.Size(149, 109);
      this.dataBox.TabIndex = 5;
      // 
      // trackBar1
      // 
      this.trackBar1.Location = new System.Drawing.Point(33, 92);
      this.trackBar1.Maximum = 100;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(104, 45);
      this.trackBar1.SmallChange = 10;
      this.trackBar1.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(33, 123);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(27, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Fast";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(111, 123);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(30, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Slow";
      this.label2.Click += new System.EventHandler(this.label2_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(33, 189);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(0, 13);
      this.label3.TabIndex = 9;
      this.label3.Click += new System.EventHandler(this.label3_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(550, 395);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.trackBar1);
      this.Controls.Add(this.dataBox);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.sudokuDisplay);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "Sudoku Solver";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.TextBox sudokuDisplay;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TextBox dataBox;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.TrackBar trackBar1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
  }
}

