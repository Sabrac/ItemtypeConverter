// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.FormBinaryFile
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace cq_itemtypeToItemtypeDat
{
  public class FormBinaryFile : Form
  {
    private bool openFile = false;
    private IContainer components = (IContainer) null;
    private string[] vsfLines;
    private string valueSeperator;
    private Structure structure;
    private Label lblValueSeperatedFile;
    private TextBox tbVSFName;
    private Button btnVSFSelect;
    private Label lblStructureFile;
    private TextBox tbStructureFileName;
    private Button btnStructureFileSelect;
    private Label lblSeperator;
    private ComboBox cbValueSeperator;
    private Button btnCancel;
    private Button btnCreate;
    private OpenFileDialog ofdVSFDialog;
    private OpenFileDialog ofdStructureFileDialog;
    private SaveFileDialog sfdSaveBinaryFile;

    public FormBinaryFile()
    {
      this.InitializeComponent();
      this.cbValueSeperator.SelectedIndex = 0;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to close without converting the data into a binary file?", "Close", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.Close();
    }

    private void btnVSFSelect_Click(object sender, EventArgs e)
    {
      int num = (int) this.ofdVSFDialog.ShowDialog();
      if (!this.openFile)
        return;
      this.openFile = false;
      this.vsfLines = File.ReadAllLines(this.ofdVSFDialog.FileName);
      this.tbVSFName.Text = this.ofdVSFDialog.FileName;
    }

    private void btnStructureFileSelect_Click(object sender, EventArgs e)
    {
      int num = (int) this.ofdStructureFileDialog.ShowDialog();
      if (!this.openFile)
        return;
      this.openFile = false;
      this.structure = new Structure(this.ofdStructureFileDialog.FileName);
      this.tbStructureFileName.Text = this.ofdStructureFileDialog.FileName;
    }

    private void ofdVSFDialog_FileOk(object sender, CancelEventArgs e)
    {
      this.openFile = true;
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
      int num = (int) this.sfdSaveBinaryFile.ShowDialog();
      if (!this.openFile)
        return;
      this.openFile = false;
      new Itemtype(this.structure, this.vsfLines, this.valueSeperator).SaveDatFile(this.sfdSaveBinaryFile.FileName);
      this.Close();
    }

    private void cbValueSeperator_TextChanged(object sender, EventArgs e)
    {
      this.valueSeperator = this.cbValueSeperator.Text.Replace("%tab", "\t").Replace("%space", " ").Replace("%%", "%");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblValueSeperatedFile = new Label();
      this.tbVSFName = new TextBox();
      this.btnVSFSelect = new Button();
      this.lblStructureFile = new Label();
      this.tbStructureFileName = new TextBox();
      this.btnStructureFileSelect = new Button();
      this.lblSeperator = new Label();
      this.cbValueSeperator = new ComboBox();
      this.btnCancel = new Button();
      this.btnCreate = new Button();
      this.ofdVSFDialog = new OpenFileDialog();
      this.ofdStructureFileDialog = new OpenFileDialog();
      this.sfdSaveBinaryFile = new SaveFileDialog();
      this.SuspendLayout();
      this.lblValueSeperatedFile.AutoSize = true;
      this.lblValueSeperatedFile.Location = new Point(12, 9);
      this.lblValueSeperatedFile.Name = "lblValueSeperatedFile";
      this.lblValueSeperatedFile.Size = new Size(108, 13);
      this.lblValueSeperatedFile.TabIndex = 0;
      this.lblValueSeperatedFile.Text = "Value Seperated File:";
      this.tbVSFName.Location = new Point(12, 25);
      this.tbVSFName.Name = "tbVSFName";
      this.tbVSFName.Size = new Size(528, 20);
      this.tbVSFName.TabIndex = 1;
      this.btnVSFSelect.Location = new Point(546, 25);
      this.btnVSFSelect.Name = "btnVSFSelect";
      this.btnVSFSelect.Size = new Size(36, 23);
      this.btnVSFSelect.TabIndex = 2;
      this.btnVSFSelect.Text = "...";
      this.btnVSFSelect.UseVisualStyleBackColor = true;
      this.btnVSFSelect.Click += new EventHandler(this.btnVSFSelect_Click);
      this.lblStructureFile.AutoSize = true;
      this.lblStructureFile.Location = new Point(12, 48);
      this.lblStructureFile.Name = "lblStructureFile";
      this.lblStructureFile.Size = new Size(72, 13);
      this.lblStructureFile.TabIndex = 3;
      this.lblStructureFile.Text = "Structure File:";
      this.tbStructureFileName.Location = new Point(12, 64);
      this.tbStructureFileName.Name = "tbStructureFileName";
      this.tbStructureFileName.Size = new Size(528, 20);
      this.tbStructureFileName.TabIndex = 1;
      this.btnStructureFileSelect.Location = new Point(546, 62);
      this.btnStructureFileSelect.Name = "btnStructureFileSelect";
      this.btnStructureFileSelect.Size = new Size(36, 23);
      this.btnStructureFileSelect.TabIndex = 2;
      this.btnStructureFileSelect.Text = "...";
      this.btnStructureFileSelect.UseVisualStyleBackColor = true;
      this.btnStructureFileSelect.Click += new EventHandler(this.btnStructureFileSelect_Click);
      this.lblSeperator.AutoSize = true;
      this.lblSeperator.Location = new Point(12, 87);
      this.lblSeperator.Name = "lblSeperator";
      this.lblSeperator.Size = new Size(86, 13);
      this.lblSeperator.TabIndex = 4;
      this.lblSeperator.Text = "Value Seperator:";
      this.cbValueSeperator.FormattingEnabled = true;
      this.cbValueSeperator.Items.AddRange(new object[3]
      {
        (object) "%tab",
        (object) "%space",
        (object) "%%"
      });
      this.cbValueSeperator.Location = new Point(12, 103);
      this.cbValueSeperator.Name = "cbValueSeperator";
      this.cbValueSeperator.Size = new Size(249, 21);
      this.cbValueSeperator.TabIndex = 5;
      this.cbValueSeperator.TextChanged += new EventHandler(this.cbValueSeperator_TextChanged);
      this.btnCancel.Location = new Point(507, 101);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnCreate.Location = new Point(267, 101);
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new Size(234, 23);
      this.btnCreate.TabIndex = 7;
      this.btnCreate.Text = "Create Converted Binary File";
      this.btnCreate.UseVisualStyleBackColor = true;
      this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
      this.ofdVSFDialog.Filter = "Text Files|*.txt|All Files|*.*";
      this.ofdVSFDialog.FileOk += new CancelEventHandler(this.ofdVSFDialog_FileOk);
      this.ofdStructureFileDialog.Filter = "Config Files|*.ini";
      this.ofdStructureFileDialog.FileOk += new CancelEventHandler(this.ofdVSFDialog_FileOk);
      this.sfdSaveBinaryFile.Filter = "Data Files|*.dat|All Files|*.*";
      this.sfdSaveBinaryFile.FileOk += new CancelEventHandler(this.ofdVSFDialog_FileOk);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(588, 134);
      this.Controls.Add((Control) this.btnCreate);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.cbValueSeperator);
      this.Controls.Add((Control) this.lblSeperator);
      this.Controls.Add((Control) this.lblStructureFile);
      this.Controls.Add((Control) this.btnStructureFileSelect);
      this.Controls.Add((Control) this.btnVSFSelect);
      this.Controls.Add((Control) this.tbStructureFileName);
      this.Controls.Add((Control) this.tbVSFName);
      this.Controls.Add((Control) this.lblValueSeperatedFile);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "FormBinaryFile";
      this.Text = "FormBinaryFile";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
