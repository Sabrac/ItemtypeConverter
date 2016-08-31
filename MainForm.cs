// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.MainForm
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cq_itemtypeToItemtypeDat
{
  public class MainForm : Form
  {
    private bool openFile = false;
    private IContainer components = (IContainer) null;
    private Itemtype itemtype;
    private MainMenu mainMenu1;
    private MenuItem menuItem1;
    private MenuItem menuItem2;
    private MenuItem miNewBinaryStructure;
    private MenuItem miNewBinaryFile;
    private MenuItem menuItem3;
    private MenuItem miOpenBinaryStructure;
    private OpenFileDialog ofdOpenBinaryStructureFile;
    private MenuItem miExit;
    private LinkLabel lblCodedBy;

    public MainForm()
    {
      this.InitializeComponent();
    }

    private void miNewBinaryStructure_Click(object sender, EventArgs e)
    {
      int num = (int) new FormBinaryStructure().ShowDialog();
    }

    private void miOpenBinaryStructure_Click(object sender, EventArgs e)
    {
      int num1 = (int) this.ofdOpenBinaryStructureFile.ShowDialog();
      if (!this.openFile)
        return;
      this.openFile = false;
      int num2 = (int) new FormBinaryStructure(this.ofdOpenBinaryStructureFile.FileName).ShowDialog();
    }

    private void ofdOpenBinaryStructureFile_FileOk(object sender, CancelEventArgs e)
    {
      this.openFile = true;
    }

    private void miNewBinaryFile_Click(object sender, EventArgs e)
    {
      int num = (int) new FormBinaryFile().ShowDialog();
    }

    private void miExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.mainMenu1 = new MainMenu(this.components);
      this.menuItem1 = new MenuItem();
      this.menuItem2 = new MenuItem();
      this.miNewBinaryStructure = new MenuItem();
      this.miNewBinaryFile = new MenuItem();
      this.menuItem3 = new MenuItem();
      this.miOpenBinaryStructure = new MenuItem();
      this.ofdOpenBinaryStructureFile = new OpenFileDialog();
      this.miExit = new MenuItem();
      this.lblCodedBy = new LinkLabel();
      this.SuspendLayout();
      this.mainMenu1.MenuItems.AddRange(new MenuItem[1]
      {
        this.menuItem1
      });
      this.menuItem1.Index = 0;
      this.menuItem1.MenuItems.AddRange(new MenuItem[3]
      {
        this.menuItem2,
        this.menuItem3,
        this.miExit
      });
      this.menuItem1.Text = "File";
      this.menuItem2.Index = 0;
      this.menuItem2.MenuItems.AddRange(new MenuItem[2]
      {
        this.miNewBinaryStructure,
        this.miNewBinaryFile
      });
      this.menuItem2.Text = "New...";
      this.miNewBinaryStructure.Index = 0;
      this.miNewBinaryStructure.Text = "Binary Structure";
      this.miNewBinaryStructure.Click += new EventHandler(this.miNewBinaryStructure_Click);
      this.miNewBinaryFile.Index = 1;
      this.miNewBinaryFile.Text = "Binary File";
      this.miNewBinaryFile.Click += new EventHandler(this.miNewBinaryFile_Click);
      this.menuItem3.Index = 1;
      this.menuItem3.MenuItems.AddRange(new MenuItem[1]
      {
        this.miOpenBinaryStructure
      });
      this.menuItem3.Text = "Open...";
      this.miOpenBinaryStructure.Index = 0;
      this.miOpenBinaryStructure.Text = "Binary Structure";
      this.miOpenBinaryStructure.Click += new EventHandler(this.miOpenBinaryStructure_Click);
      this.ofdOpenBinaryStructureFile.Filter = "Config Files|*.ini";
      this.ofdOpenBinaryStructureFile.FileOk += new CancelEventHandler(this.ofdOpenBinaryStructureFile_FileOk);
      this.miExit.Index = 2;
      this.miExit.Text = "Exit";
      this.miExit.Click += new EventHandler(this.miExit_Click);
      this.lblCodedBy.AutoSize = true;
      this.lblCodedBy.Location = new Point(12, 9);
      this.lblCodedBy.Name = "lblCodedBy";
      this.lblCodedBy.Size = new Size(162, 39);
      this.lblCodedBy.TabIndex = 0;
      this.lblCodedBy.TabStop = true;
      this.lblCodedBy.Text = "Coded By:\r\nfunhacker www.elitepvpers.com \r\nBiG-MaC www.acmeeo.com";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(219, 79);
      this.Controls.Add((Control) this.lblCodedBy);
      this.Menu = this.mainMenu1;
      this.Name = "MainForm";
      this.Text = "Table Convertor";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
