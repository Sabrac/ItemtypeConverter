// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.FormBinaryStructure
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cq_itemtypeToItemtypeDat
{
  public class FormBinaryStructure : Form
  {
    private readonly Dictionary<string, string> help = new Dictionary<string, string>()
    {
      {
        "Default",
        "Help:\r\nHover the mouse over an option for more help."
      },
      {
        "chbHasEntryCount",
        "Has Entry Count:\r\nDefines whether or not the binary file will write a UINT value of the amount of 'Entries' in the binary file in the first 4 bytes of the file. If unsure leave this checked."
      },
      {
        "chbHasIndexTable",
        "Has Index Table:\r\nDefines whether or not the binary file will have an index table. This is a list of primary keys (selected in the index field combo box) written before the entry data. An example of this being used is the Itemtype.dat. If unsure leave this checked."
      },
      {
        "cbIndexField",
        "Index Field Name:\r\nDefines the name of the field that is used for creating an index table (if applicable). The field chosen will use its data type when being written aswell."
      },
      {
        "lblDefaultByte",
        "Default Byte:\r\nDefines the default byte value to be used when no other value is known. This will be used to fill in blank/unknown areas of the binary file, this will also be used when a character array doesn't contain the predetermined amount of characters. Use 0 if unsure."
      },
      {
        "cbPrefferedDataType",
        "Preffered Data Type:\r\nDefines what field value should be used if two or more fields fall onto the same byte offset. An example is Hitrate and Monstertype occupy the same space in the Itemtype.dat. You can choose First if you wish to always use Hitrate. Or you can use Biggest, if you wish to use the biggest of two values. If unsure always use Biggest."
      },
      {
        "lbFields",
        "Fields:\r\nA list of all the fields that have been defined in the structure. Select one to edit/delete it, or you can select it to get similar values to add a new field. No field can have the exact same name."
      },
      {
        "btnDelete",
        "Delete Button:\r\nUsed to delete an existing field from the structure. Be sure to first select a field within the fields list before clicking. This can not be undone..."
      },
      {
        "tbFieldName",
        "Field Name:\r\nUsed to identify the field in question, this should match the field in the MySQL table exactly."
      },
      {
        "cbType",
        "Data Type:\r\nDefines the type of data the field will contain. This should match what the client will read, and also what the MySQL table will hold, to prevent any errors. If unsure don't modify, only download existing structure files."
      },
      {
        "lblSize",
        "Array Size:\r\nDefines the size of a character array. This field is of no use unless definining a character array that must be an exact size. An example is the Name and Description of an item in the Itemtype.dat."
      },
      {
        "lblByteOffset",
        "Byte Offset:\r\nDefines the location of the data within the entry data block. If unsure do not modify, only download existing structure files."
      },
      {
        "lblTableFieldIndex",
        "Table Field Index:\r\nDefines the index of the field within the table that is being used to convert to a binary file. Note field indicies are calculated from left to right, 0 -> n where n is the highest index."
      },
      {
        "btnAdd",
        "Add Button:\r\nUsed to add a new field to the structure. Be sure that the field name does not already exist as only unique field names can be added to the structure."
      },
      {
        "btnUpdate",
        "Update Button:\r\nUsed to make changes to any existing field. At the moment field names can not be changed. To do that you should first delete the field then add it again with the new name."
      },
      {
        "btnSave",
        "Save Button:\r\nUsed to save changes to the structure file. This will open a save file dialog."
      },
      {
        "btnCancel",
        "Cancel Button:\r\nUsed to cancel any changes and close the dialog. This can not be undone."
      },
      {
        "lblEntrySize",
        "Entry Size:\r\nDefines how large (in bytes) an entry is. If unsure add all the fields up."
      }
    };
    private IContainer components = (IContainer) null;
    private const PrefferedDataType defaultType = PrefferedDataType.Biggest;
    private Structure structure;
    private Label lblFields;
    private ListBox lbFields;
    private GroupBox gbFieldValues;
    private NumericUpDown nudTableFieldIndex;
    private Label lblTableFieldIndex;
    private NumericUpDown nudByteOffset;
    private Label lblDatOffset;
    private NumericUpDown nudSize;
    private Label lblSize;
    private ComboBox cbType;
    private Label lblType;
    private Button btnUpdate;
    private Button btnAdd;
    private TextBox tbFieldName;
    private Label lblFieldName;
    private Button btnDelete;
    private SaveFileDialog sfdSaveBinaryStructureFile;
    private GroupBox gbMainSettings;
    private ComboBox cbIndexField;
    private Label lblIndexField;
    private CheckBox chbHasIndexTable;
    private CheckBox chbHasEntryCount;
    private NumericUpDown nudDefaultByte;
    private Label lblDefaultByte;
    private ComboBox cbPrefferedDataType;
    private Label lblPrefferedDataType;
    private Label lblHelp;
    private Button btnSave;
    private Button btnCancel;
    private NumericUpDown nudEntrySize;
    private Label lblEntrySize;

    public FormBinaryStructure()
    {
      this.InitializeComponent();
      this.cbType.Items.AddRange((object[]) Enum.GetNames(typeof (FieldType)));
      this.cbPrefferedDataType.Items.AddRange((object[]) Enum.GetNames(typeof (PrefferedDataType)));
      this.cbPrefferedDataType.SelectedItem = (object) PrefferedDataType.Biggest.ToString();
      this.structure = new Structure();
    }

    public FormBinaryStructure(string fileName)
      : this()
    {
      this.structure = new Structure(fileName);
      this.populateFieldLists();
      this.chbHasEntryCount.Checked = this.structure.HasEntryCount;
      this.chbHasIndexTable.Checked = this.structure.HasIndexTable;
      this.cbIndexField.SelectedItem = (object) this.structure.IndexFieldName;
      this.nudDefaultByte.Value = (Decimal) this.structure.DefaultByte;
      this.cbPrefferedDataType.SelectedItem = (object) this.structure.PrefferedDataType.ToString();
      this.nudEntrySize.Value = (Decimal) this.structure.EntrySize;
    }

    private void populateFieldLists()
    {
      int num1 = this.lbFields.SelectedIndex;
      int num2 = this.cbIndexField.SelectedIndex;
      this.lbFields.Items.Clear();
      this.cbIndexField.Items.Clear();
      List<string> list = this.structure.Fields.Keys.ToList<string>();
      if (list.Count < 1)
        return;
      this.lbFields.Items.AddRange((object[]) list.ToArray());
      this.cbIndexField.Items.AddRange((object[]) list.ToArray());
      if (num1 >= this.lbFields.Items.Count)
        num1 = this.lbFields.Items.Count - 1;
      if (num2 >= this.cbIndexField.Items.Count)
        num2 = this.cbIndexField.Items.Count - 1;
      this.lbFields.SelectedIndex = num1;
      this.cbIndexField.SelectedIndex = num2;
      if (this.cbIndexField.Items.Count != 1)
        return;
      this.cbIndexField.SelectedIndex = 0;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to close without saving changes?", "Close", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.Close();
    }

    private void cbIndexField_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.chbHasIndexTable.Checked = this.cbIndexField.SelectedIndex >= 0 && this.cbIndexField.SelectedIndex < this.cbIndexField.Items.Count;
    }

    private void chbHasIndexTable_CheckedChanged(object sender, EventArgs e)
    {
      this.cbIndexField.Enabled = this.chbHasIndexTable.Checked;
      if (!this.chbHasIndexTable.Checked || this.cbIndexField.SelectedIndex != -1)
        return;
      this.cbIndexField.SelectedIndex = 0;
    }

    private void lbFields_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!this.structure.Fields.ContainsKey(this.lbFields.SelectedItem.ToString()))
        return;
      FieldStructure field = this.structure.Fields[this.lbFields.SelectedItem.ToString()];
      this.tbFieldName.Text = this.lbFields.SelectedItem.ToString();
      this.cbIndexField.SelectedItem = (object) field.Type;
      this.nudSize.Value = (Decimal) field.ArraySize;
      this.nudByteOffset.Value = (Decimal) field.ByteOffset;
      this.nudTableFieldIndex.Value = (Decimal) field.FieldIndex;
    }

    private void cbType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if ((FieldType) Enum.Parse(typeof (FieldType), this.cbType.SelectedItem.ToString()) == FieldType.CHARARRAY)
        this.nudSize.Enabled = true;
      else
        this.nudSize.Enabled = false;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (this.structure.Fields.ContainsKey(this.tbFieldName.Text))
        return;
      string text = this.tbFieldName.Text;
      string _type = this.cbType.SelectedItem.ToString();
      string _byteOffset = this.nudByteOffset.Value.ToString();
      Decimal num = this.nudTableFieldIndex.Value;
      string _fieldIndex = num.ToString();
      string _arraySize;
      if (this.cbType.SelectedItem != (object) FieldType.CHARARRAY.ToString())
      {
        _arraySize = (string) null;
      }
      else
      {
        num = this.nudSize.Value;
        _arraySize = num.ToString();
      }
      this.structure.Fields.Add(this.tbFieldName.Text, new FieldStructure(text, _type, _byteOffset, _fieldIndex, _arraySize));
      this.populateFieldLists();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
      if (this.lbFields.SelectedIndex < 0 || this.lbFields.SelectedIndex >= this.lbFields.Items.Count)
        return;
      if (this.lbFields.SelectedItem.ToString() == this.tbFieldName.Text)
      {
        FieldStructure field = this.structure.Fields[this.lbFields.SelectedItem.ToString()];
        field.Type = (FieldType) Enum.Parse(typeof (FieldType), this.cbIndexField.SelectedItem.ToString());
        if (field.Type == FieldType.CHARARRAY)
          field.ArraySize = (int) this.nudSize.Value;
        field.ByteOffset = (long) this.nudSize.Value;
        field.FieldIndex = (int) this.nudTableFieldIndex.Value;
      }
      this.populateFieldLists();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (this.lbFields.SelectedIndex < 0 || this.lbFields.SelectedIndex >= this.lbFields.Items.Count)
        return;
      this.structure.Fields.Remove(this.lbFields.SelectedItem.ToString());
      if (this.cbIndexField.SelectedItem.ToString() == this.lbFields.SelectedItem.ToString())
      {
        if (this.cbIndexField.Items.Count != 1)
          this.cbIndexField.SelectedIndex = 0;
        else
          this.cbIndexField.SelectedIndex = -1;
      }
      this.populateFieldLists();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      int num = (int) this.sfdSaveBinaryStructureFile.ShowDialog();
    }

    private void sfdSaveBinaryStructureFile_FileOk(object sender, CancelEventArgs e)
    {
      this.structure.Save(this.sfdSaveBinaryStructureFile.FileName);
      this.Close();
    }

    private void MouseEnter_Event(object sender, EventArgs e)
    {
      if (this.help.ContainsKey(((Control) sender).Name))
        this.lblHelp.Text = this.help[((Control) sender).Name];
      else
        this.lblHelp.Text = this.help["Default"];
    }

    private void MouseLeave_Event(object sender, EventArgs e)
    {
      this.lblHelp.Text = this.help["Default"];
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblFields = new Label();
      this.lbFields = new ListBox();
      this.gbFieldValues = new GroupBox();
      this.btnUpdate = new Button();
      this.btnAdd = new Button();
      this.tbFieldName = new TextBox();
      this.lblFieldName = new Label();
      this.nudTableFieldIndex = new NumericUpDown();
      this.nudByteOffset = new NumericUpDown();
      this.nudSize = new NumericUpDown();
      this.cbType = new ComboBox();
      this.lblType = new Label();
      this.lblTableFieldIndex = new Label();
      this.lblDatOffset = new Label();
      this.lblSize = new Label();
      this.btnDelete = new Button();
      this.sfdSaveBinaryStructureFile = new SaveFileDialog();
      this.gbMainSettings = new GroupBox();
      this.cbPrefferedDataType = new ComboBox();
      this.lblPrefferedDataType = new Label();
      this.nudDefaultByte = new NumericUpDown();
      this.cbIndexField = new ComboBox();
      this.lblIndexField = new Label();
      this.chbHasIndexTable = new CheckBox();
      this.chbHasEntryCount = new CheckBox();
      this.lblDefaultByte = new Label();
      this.lblHelp = new Label();
      this.btnSave = new Button();
      this.btnCancel = new Button();
      this.lblEntrySize = new Label();
      this.nudEntrySize = new NumericUpDown();
      this.gbFieldValues.SuspendLayout();
      this.nudTableFieldIndex.BeginInit();
      this.nudByteOffset.BeginInit();
      this.nudSize.BeginInit();
      this.gbMainSettings.SuspendLayout();
      this.nudDefaultByte.BeginInit();
      this.nudEntrySize.BeginInit();
      this.SuspendLayout();
      this.lblFields.AutoSize = true;
      this.lblFields.Location = new Point(179, 9);
      this.lblFields.Name = "lblFields";
      this.lblFields.Size = new Size(37, 13);
      this.lblFields.TabIndex = 0;
      this.lblFields.Text = "Fields:";
      this.lbFields.FormattingEnabled = true;
      this.lbFields.Location = new Point(179, 25);
      this.lbFields.Name = "lbFields";
      this.lbFields.Size = new Size(154, 251);
      this.lbFields.TabIndex = 1;
      this.lbFields.SelectedIndexChanged += new EventHandler(this.lbFields_SelectedIndexChanged);
      this.lbFields.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.lbFields.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.gbFieldValues.Controls.Add((Control) this.btnUpdate);
      this.gbFieldValues.Controls.Add((Control) this.btnAdd);
      this.gbFieldValues.Controls.Add((Control) this.tbFieldName);
      this.gbFieldValues.Controls.Add((Control) this.lblFieldName);
      this.gbFieldValues.Controls.Add((Control) this.nudTableFieldIndex);
      this.gbFieldValues.Controls.Add((Control) this.nudByteOffset);
      this.gbFieldValues.Controls.Add((Control) this.nudSize);
      this.gbFieldValues.Controls.Add((Control) this.cbType);
      this.gbFieldValues.Controls.Add((Control) this.lblType);
      this.gbFieldValues.Controls.Add((Control) this.lblTableFieldIndex);
      this.gbFieldValues.Controls.Add((Control) this.lblDatOffset);
      this.gbFieldValues.Controls.Add((Control) this.lblSize);
      this.gbFieldValues.Location = new Point(339, 25);
      this.gbFieldValues.Name = "gbFieldValues";
      this.gbFieldValues.Size = new Size(138, 291);
      this.gbFieldValues.TabIndex = 2;
      this.gbFieldValues.TabStop = false;
      this.gbFieldValues.Text = "Field Values";
      this.btnUpdate.Location = new Point(6, 257);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new Size(120, 23);
      this.btnUpdate.TabIndex = 11;
      this.btnUpdate.Text = "Update Field";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
      this.btnUpdate.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.btnUpdate.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.btnAdd.Location = new Point(6, 222);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new Size(120, 23);
      this.btnAdd.TabIndex = 10;
      this.btnAdd.Text = "Add Field";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
      this.btnAdd.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.btnAdd.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.tbFieldName.Location = new Point(6, 32);
      this.tbFieldName.Name = "tbFieldName";
      this.tbFieldName.Size = new Size(120, 20);
      this.tbFieldName.TabIndex = 9;
      this.tbFieldName.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.tbFieldName.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblFieldName.AutoSize = true;
      this.lblFieldName.Location = new Point(6, 16);
      this.lblFieldName.Name = "lblFieldName";
      this.lblFieldName.Size = new Size(63, 13);
      this.lblFieldName.TabIndex = 8;
      this.lblFieldName.Text = "Field Name:";
      this.nudTableFieldIndex.Location = new Point(6, 189);
      NumericUpDown nudTableFieldIndex = this.nudTableFieldIndex;
      int[] bits1 = new int[4];
      bits1[0] = 100000;
      Decimal num1 = new Decimal(bits1);
      nudTableFieldIndex.Maximum = num1;
      this.nudTableFieldIndex.Name = "nudTableFieldIndex";
      this.nudTableFieldIndex.Size = new Size(120, 20);
      this.nudTableFieldIndex.TabIndex = 7;
      this.nudByteOffset.Location = new Point(6, 150);
      this.nudByteOffset.Maximum = new Decimal(new int[4]
      {
        1215752192,
        23,
        0,
        0
      });
      this.nudByteOffset.Name = "nudByteOffset";
      this.nudByteOffset.Size = new Size(120, 20);
      this.nudByteOffset.TabIndex = 5;
      this.nudSize.Location = new Point(6, 111);
      NumericUpDown nudSize1 = this.nudSize;
      int[] bits2 = new int[4];
      bits2[0] = 100000;
      Decimal num2 = new Decimal(bits2);
      nudSize1.Maximum = num2;
      NumericUpDown nudSize2 = this.nudSize;
      int[] bits3 = new int[4];
      bits3[0] = 1;
      Decimal num3 = new Decimal(bits3);
      nudSize2.Minimum = num3;
      this.nudSize.Name = "nudSize";
      this.nudSize.Size = new Size(120, 20);
      this.nudSize.TabIndex = 3;
      NumericUpDown nudSize3 = this.nudSize;
      int[] bits4 = new int[4];
      bits4[0] = 1;
      Decimal num4 = new Decimal(bits4);
      nudSize3.Value = num4;
      this.cbType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbType.FormattingEnabled = true;
      this.cbType.Location = new Point(6, 71);
      this.cbType.Name = "cbType";
      this.cbType.Size = new Size(120, 21);
      this.cbType.TabIndex = 1;
      this.cbType.SelectedIndexChanged += new EventHandler(this.cbType_SelectedIndexChanged);
      this.cbType.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.cbType.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblType.AutoSize = true;
      this.lblType.Location = new Point(6, 55);
      this.lblType.Name = "lblType";
      this.lblType.Size = new Size(34, 13);
      this.lblType.TabIndex = 0;
      this.lblType.Text = "Type:";
      this.lblTableFieldIndex.BackColor = SystemColors.Control;
      this.lblTableFieldIndex.Location = new Point(6, 173);
      this.lblTableFieldIndex.Name = "lblTableFieldIndex";
      this.lblTableFieldIndex.Size = new Size(120, 36);
      this.lblTableFieldIndex.TabIndex = 6;
      this.lblTableFieldIndex.Text = "Table Field Index:";
      this.lblTableFieldIndex.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.lblTableFieldIndex.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblDatOffset.BackColor = SystemColors.Control;
      this.lblDatOffset.Location = new Point(6, 134);
      this.lblDatOffset.Name = "lblDatOffset";
      this.lblDatOffset.Size = new Size(120, 36);
      this.lblDatOffset.TabIndex = 4;
      this.lblDatOffset.Text = "Byte Offset:";
      this.lblDatOffset.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.lblDatOffset.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblSize.BackColor = SystemColors.Control;
      this.lblSize.Location = new Point(6, 95);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new Size(120, 36);
      this.lblSize.TabIndex = 2;
      this.lblSize.Text = "Size:";
      this.lblSize.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.lblSize.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.btnDelete.Location = new Point(179, 282);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new Size(154, 23);
      this.btnDelete.TabIndex = 3;
      this.btnDelete.Text = "Delete Field";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.btnDelete.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.btnDelete.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.sfdSaveBinaryStructureFile.Filter = "Config Files|*.ini";
      this.sfdSaveBinaryStructureFile.FileOk += new CancelEventHandler(this.sfdSaveBinaryStructureFile_FileOk);
      this.gbMainSettings.Controls.Add((Control) this.nudEntrySize);
      this.gbMainSettings.Controls.Add((Control) this.lblEntrySize);
      this.gbMainSettings.Controls.Add((Control) this.cbPrefferedDataType);
      this.gbMainSettings.Controls.Add((Control) this.lblPrefferedDataType);
      this.gbMainSettings.Controls.Add((Control) this.nudDefaultByte);
      this.gbMainSettings.Controls.Add((Control) this.cbIndexField);
      this.gbMainSettings.Controls.Add((Control) this.lblIndexField);
      this.gbMainSettings.Controls.Add((Control) this.chbHasIndexTable);
      this.gbMainSettings.Controls.Add((Control) this.chbHasEntryCount);
      this.gbMainSettings.Controls.Add((Control) this.lblDefaultByte);
      this.gbMainSettings.Location = new Point(12, 12);
      this.gbMainSettings.Name = "gbMainSettings";
      this.gbMainSettings.Size = new Size(161, 293);
      this.gbMainSettings.TabIndex = 4;
      this.gbMainSettings.TabStop = false;
      this.gbMainSettings.Text = "Main Settings";
      this.cbPrefferedDataType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbPrefferedDataType.FormattingEnabled = true;
      this.cbPrefferedDataType.Location = new Point(6, 157);
      this.cbPrefferedDataType.Name = "cbPrefferedDataType";
      this.cbPrefferedDataType.Size = new Size(121, 21);
      this.cbPrefferedDataType.TabIndex = 7;
      this.cbPrefferedDataType.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.cbPrefferedDataType.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblPrefferedDataType.AutoSize = true;
      this.lblPrefferedDataType.Location = new Point(6, 141);
      this.lblPrefferedDataType.Name = "lblPrefferedDataType";
      this.lblPrefferedDataType.Size = new Size(106, 13);
      this.lblPrefferedDataType.TabIndex = 6;
      this.lblPrefferedDataType.Text = "Preffered Data Type:";
      this.nudDefaultByte.Location = new Point(6, 118);
      NumericUpDown nudDefaultByte = this.nudDefaultByte;
      int[] bits5 = new int[4];
      bits5[0] = (int) byte.MaxValue;
      Decimal num5 = new Decimal(bits5);
      nudDefaultByte.Maximum = num5;
      this.nudDefaultByte.Name = "nudDefaultByte";
      this.nudDefaultByte.Size = new Size(121, 20);
      this.nudDefaultByte.TabIndex = 5;
      this.cbIndexField.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbIndexField.Enabled = false;
      this.cbIndexField.FormattingEnabled = true;
      this.cbIndexField.Location = new Point(6, 78);
      this.cbIndexField.Name = "cbIndexField";
      this.cbIndexField.Size = new Size(121, 21);
      this.cbIndexField.TabIndex = 3;
      this.cbIndexField.SelectedIndexChanged += new EventHandler(this.cbIndexField_SelectedIndexChanged);
      this.cbIndexField.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.cbIndexField.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblIndexField.AutoSize = true;
      this.lblIndexField.Location = new Point(6, 62);
      this.lblIndexField.Name = "lblIndexField";
      this.lblIndexField.Size = new Size(61, 13);
      this.lblIndexField.TabIndex = 2;
      this.lblIndexField.Text = "Index Field:";
      this.chbHasIndexTable.AutoSize = true;
      this.chbHasIndexTable.Location = new Point(6, 42);
      this.chbHasIndexTable.Name = "chbHasIndexTable";
      this.chbHasIndexTable.RightToLeft = RightToLeft.Yes;
      this.chbHasIndexTable.Size = new Size(104, 17);
      this.chbHasIndexTable.TabIndex = 1;
      this.chbHasIndexTable.Text = "Has Index Table";
      this.chbHasIndexTable.UseVisualStyleBackColor = true;
      this.chbHasIndexTable.CheckedChanged += new EventHandler(this.chbHasIndexTable_CheckedChanged);
      this.chbHasIndexTable.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.chbHasIndexTable.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.chbHasEntryCount.AutoSize = true;
      this.chbHasEntryCount.Checked = true;
      this.chbHasEntryCount.CheckState = CheckState.Checked;
      this.chbHasEntryCount.Location = new Point(6, 19);
      this.chbHasEntryCount.Name = "chbHasEntryCount";
      this.chbHasEntryCount.RightToLeft = RightToLeft.Yes;
      this.chbHasEntryCount.Size = new Size(103, 17);
      this.chbHasEntryCount.TabIndex = 0;
      this.chbHasEntryCount.Text = "Has Entry Count";
      this.chbHasEntryCount.UseVisualStyleBackColor = true;
      this.chbHasEntryCount.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.chbHasEntryCount.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblDefaultByte.BackColor = SystemColors.Control;
      this.lblDefaultByte.Location = new Point(6, 102);
      this.lblDefaultByte.Name = "lblDefaultByte";
      this.lblDefaultByte.Size = new Size(121, 36);
      this.lblDefaultByte.TabIndex = 4;
      this.lblDefaultByte.Text = "Default Byte:";
      this.lblDefaultByte.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.lblDefaultByte.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblHelp.BorderStyle = BorderStyle.FixedSingle;
      this.lblHelp.Location = new Point(17, 348);
      this.lblHelp.Name = "lblHelp";
      this.lblHelp.Size = new Size(459, 97);
      this.lblHelp.TabIndex = 5;
      this.lblHelp.Text = "Hover the mouse over an option for more help.";
      this.btnSave.Location = new Point(321, 322);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(75, 23);
      this.btnSave.TabIndex = 6;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnSave.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.btnSave.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.btnCancel.Location = new Point(402, 322);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnCancel.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.btnCancel.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.lblEntrySize.Location = new Point(6, 181);
      this.lblEntrySize.Name = "lblEntrySize";
      this.lblEntrySize.Size = new Size(121, 36);
      this.lblEntrySize.TabIndex = 8;
      this.lblEntrySize.Text = "Entry Size:";
      this.lblEntrySize.MouseEnter += new EventHandler(this.MouseEnter_Event);
      this.lblEntrySize.MouseLeave += new EventHandler(this.MouseLeave_Event);
      this.nudEntrySize.Location = new Point(6, 197);
      NumericUpDown nudEntrySize = this.nudEntrySize;
      int[] bits6 = new int[4];
      bits6[0] = 10000000;
      Decimal num6 = new Decimal(bits6);
      nudEntrySize.Maximum = num6;
      this.nudEntrySize.Name = "nudEntrySize";
      this.nudEntrySize.Size = new Size(121, 20);
      this.nudEntrySize.TabIndex = 9;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(481, 452);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnSave);
      this.Controls.Add((Control) this.lblHelp);
      this.Controls.Add((Control) this.gbMainSettings);
      this.Controls.Add((Control) this.btnDelete);
      this.Controls.Add((Control) this.gbFieldValues);
      this.Controls.Add((Control) this.lbFields);
      this.Controls.Add((Control) this.lblFields);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "FormBinaryStructure";
      this.Text = "FormBinaryStructure";
      this.gbFieldValues.ResumeLayout(false);
      this.gbFieldValues.PerformLayout();
      this.nudTableFieldIndex.EndInit();
      this.nudByteOffset.EndInit();
      this.nudSize.EndInit();
      this.gbMainSettings.ResumeLayout(false);
      this.gbMainSettings.PerformLayout();
      this.nudDefaultByte.EndInit();
      this.nudEntrySize.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
